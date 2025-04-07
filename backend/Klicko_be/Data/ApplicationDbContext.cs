using System.Reflection.Emit;
using Klicko_be.Models;
using Klicko_be.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<
            ApplicationUser,
            ApplicationRole,
            string,
            IdentityUserClaim<string>,
            ApplicationUserRole,
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>,
            IdentityUserToken<string>
        >
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // tabelle per gestione autenticazione
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        // tabelle per funzionalità e-commerce
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<CarryWith> CarryWiths { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderExperience> OrderExperiences { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartExperience> CartExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // correlazione UserRole a User
            builder
                .Entity<ApplicationUserRole>()
                .HasOne(ur => ur.ApplicationUser)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            // correlazione UserRole a Role
            builder
                .Entity<ApplicationUserRole>()
                .HasOne(ur => ur.ApplicationRole)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // correlazione Experience a Category
            builder
                .Entity<Experience>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Experiences)
                .HasForeignKey(e => e.CategoryId);

            // correlazione Image a Experience
            builder
                .Entity<Image>()
                .HasOne(i => i.Experience)
                .WithMany(e => e.Images)
                .HasForeignKey(i => i.ExperienceId);

            // correlazione CarryWith a Experience
            builder
                .Entity<CarryWith>()
                .HasOne(cw => cw.Experience)
                .WithMany(e => e.CarryWiths)
                .HasForeignKey(c => c.ExperienceId);

            // correlazione Experience a OrderExperience
            builder
                .Entity<OrderExperience>()
                .HasOne(oe => oe.Experience)
                .WithMany(e => e.OrderExperiences)
                .HasForeignKey(oe => oe.ExperienceId);

            // correlazione Order a OrderExperience
            builder
                .Entity<OrderExperience>()
                .HasOne(oe => oe.Order)
                .WithMany(o => o.OrderExperiences)
                .HasForeignKey(oe => oe.OrderId);

            // Correlazione Order a ApplicationUser
            builder
                .Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // correlazione Cart a CartExperience
            builder
                .Entity<CartExperience>()
                .HasOne(ce => ce.Cart)
                .WithMany(c => c.CartExperiences)
                .HasForeignKey(ce => ce.CartId)
                .OnDelete(DeleteBehavior.NoAction);

            // correlazione User a Cart
            builder.Entity<Cart>().HasOne(c => c.User).WithOne(u => u.Cart);

            // correlazione Experience a CartExperience
            builder
                .Entity<CartExperience>()
                .HasOne(ce => ce.Experience)
                .WithMany(e => e.CartExperiences)
                .HasForeignKey(ce => ce.ExperienceId)
                .OnDelete(DeleteBehavior.NoAction);

            // Tabella ApplicationUser
            builder
                .Entity<ApplicationUser>()
                .Property(u => u.RegistrationDate)
                .HasDefaultValue(DateTime.UtcNow);

            // Tabella Experience
            builder
                .Entity<Experience>()
                .Property(e => e.LoadingDate)
                .HasDefaultValue(DateTime.UtcNow);

            builder
                .Entity<Experience>()
                .Property(e => e.LastEditDate)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Entity<Experience>().Property(e => e.Sale).HasDefaultValue(0);

            builder.Entity<Experience>().Property(e => e.IsInEvidence).HasDefaultValue(false);

            builder.Entity<Experience>().Property(e => e.IsPopular).HasDefaultValue(false);

            builder.Entity<Experience>().Property(e => e.IsDeleted).HasDefaultValue(false);

            // Tabella Order
            builder.Entity<Order>().Property(o => o.CreatedAt).HasDefaultValue(DateTime.UtcNow);

            builder.HasSequence<int>("OrderNumber_seq").StartsAt(100000000).IncrementsBy(1);

            builder
                .Entity<Order>()
                .Property(o => o.OrderNumber)
                .HasDefaultValueSql("NEXT VALUE FOR OrderNumber_seq");

            // Tabella Cart
            builder.Entity<Cart>().Property(c => c.CreatedAt).HasDefaultValue(DateTime.UtcNow);

            builder.Entity<Cart>().Property(c => c.UpdatedAt).HasDefaultValue(DateTime.UtcNow);

            // Tabella CartExperience
            builder
                .Entity<CartExperience>()
                .Property(ce => ce.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Entity<CartExperience>().Property(ce => ce.Quantity).HasDefaultValue(1);

            // inserimento ruoli nella tabella ApplicationRoles
            builder
                .Entity<ApplicationRole>()
                .HasData(
                    new ApplicationRole()
                    {
                        Id = "8d64359a-fda6-4096-b40d-f1375775244d",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                    },
                    new ApplicationRole()
                    {
                        Id = "1f00a1d7-cbb6-44bf-bdc8-a3608b1284b9",
                        Name = "Seller",
                        NormalizedName = "SELLER",
                    },
                    new ApplicationRole()
                    {
                        Id = "849b8726-44b3-434b-9b18-48a4e8d4e9dd",
                        Name = "User",
                        NormalizedName = "USER",
                    }
                );

            // inserimento categorie nella tabella Categories
            builder
                .Entity<Category>()
                .HasData(
                    new Category()
                    {
                        CategoryId = Guid.Parse("da780fcf-074e-4e0c-b0b8-1bd8e0c0fa6f"),
                        Name = "Aria",
                        Description = "Aria",
                        Image = "aria.jpg",
                        Icon = "aria.png",
                    },
                    new Category()
                    {
                        CategoryId = Guid.Parse("6accf29d-8d1c-4edd-b48a-c70251516b99"),
                        Name = "Acqua",
                        Description = "Acqua",
                        Image = "acqua.jpg",
                        Icon = "acqua.png",
                    },
                    new Category()
                    {
                        CategoryId = Guid.Parse("6f3a957c-df09-437c-bc37-f069173eabe2"),
                        Name = "Motori",
                        Description = "Motori",
                        Image = "motori.jpg",
                        Icon = "motori.png",
                    },
                    new Category()
                    {
                        CategoryId = Guid.Parse("bda4ee71-af9c-46c6-b1bf-95f178773a2f"),
                        Name = "Trekking",
                        Description = "Trekking",
                        Image = "trekking.jpg",
                        Icon = "trekking.png",
                    },
                    new Category()
                    {
                        CategoryId = Guid.Parse("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"),
                        Name = "Gastronomia",
                        Description = "Gastronomia",
                        Image = "Gastronomia.jpg",
                        Icon = "Gastronomia.png",
                    },
                    new Category()
                    {
                        CategoryId = Guid.Parse("48733fb8-deae-41b2-b0c6-4fab3c45cf93"),
                        Name = "Arte e Cultura",
                        Description = "Arte e Cultura",
                        Image = "arteCultura.jpg",
                        Icon = "arteCultura.png",
                    },
                    new Category()
                    {
                        CategoryId = Guid.Parse("1652310e-b8f3-43e7-bd9d-287f73f939b5"),
                        Name = "Avventura",
                        Description = "Avventura",
                        Image = "avventura.jpg",
                        Icon = "avventura.png",
                    },
                    new Category()
                    {
                        CategoryId = Guid.Parse("b70671a5-3989-4e7c-9cd5-c6343e09fcde"),
                        Name = "Relax",
                        Description = "Relax",
                        Image = "relax.jpg",
                        Icon = "relax.png",
                    },
                    new Category()
                    {
                        CategoryId = Guid.Parse("a4049ef8-1e86-48bf-b514-3930469ddcbd"),
                        Name = "Sport",
                        Description = "Sport",
                        Image = "sport.jpg",
                        Icon = "sport.png",
                    },
                    new Category()
                    {
                        CategoryId = Guid.Parse("7f13b386-b8af-4ed1-b42b-845e17f657c3"),
                        Name = "Città",
                        Description = "Città",
                        Image = "citta.jpg",
                        Icon = "citta.png",
                    }
                );
        }
    }
}
