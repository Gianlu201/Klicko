using System.Text;
using Klicko_be.Data;
using Klicko_be.Models.Auth;
using Klicko_be.Services;
using Klicko_be.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configurazione Jwt
builder.Services.Configure<Jwt>(builder.Configuration.GetSection(nameof(Jwt)));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder
    .Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = builder
            .Configuration.GetSection("Identity")
            .GetValue<bool>("RequireConfirmedAccount");

        options.Password.RequiredLength = builder
            .Configuration.GetSection("Identity")
            .GetValue<int>("RequiredLength");

        options.Password.RequireDigit = builder
            .Configuration.GetSection("Identity")
            .GetValue<bool>("RequireDigit");

        options.Password.RequireLowercase = builder
            .Configuration.GetSection("Identity")
            .GetValue<bool>("RequireLowercase");

        options.Password.RequireNonAlphanumeric = builder
            .Configuration.GetSection("Identity")
            .GetValue<bool>("RequireNonAlphanumeric");

        options.Password.RequireUppercase = builder
            .Configuration.GetSection("Identity")
            .GetValue<bool>("RequireUppercase");
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("Issuer"),

            ValidAudience = builder
                .Configuration.GetSection(nameof(Jwt))
                .GetValue<string>("Audience"),

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("SecurityKey")
                )
            ),
        };
    });

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<ApplicationRole>>();

builder.Services.AddScoped<ExperienceService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
