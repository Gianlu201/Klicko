using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using Klicko_be.Models;
using Klicko_be.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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
        public DbSet<Models.Image> Images { get; set; }
        public DbSet<CarryWith> CarryWiths { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderExperience> OrderExperiences { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<FidelityCard> FidelityCards { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
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
                .Entity<Models.Image>()
                .HasOne(i => i.Experience)
                .WithMany(e => e.Images)
                .HasForeignKey(i => i.ExperienceId);

            // correlazione CarryWith a Experience
            builder
                .Entity<CarryWith>()
                .HasOne(cw => cw.Experience)
                .WithMany(e => e.CarryWiths)
                .HasForeignKey(c => c.ExperienceId);

            // correlazione Experience a ApplicationUser (UserCreator)
            builder
                .Entity<Experience>()
                .HasOne(e => e.UserCreator)
                .WithMany(u => u.ExperiencesCreated)
                .HasForeignKey(e => e.UserCreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            // correlazione Experience a ApplicationUser (UserLastModify)
            builder
                .Entity<Experience>()
                .HasOne(e => e.UserLastModify)
                .WithMany()
                .HasForeignKey(e => e.UserLastModifyId)
                .OnDelete(DeleteBehavior.NoAction);

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

            // correlazione Vaucher a Category
            builder
                .Entity<Voucher>()
                .HasOne(v => v.Category)
                .WithMany(c => c.Vouchers)
                .HasForeignKey(v => v.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // correlazione Voucher a Order
            builder
                .Entity<Voucher>()
                .HasOne(v => v.Order)
                .WithMany(o => o.Vouchers)
                .HasForeignKey(v => v.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            // correlazione User a Fidelitycard
            builder.Entity<FidelityCard>().HasOne(c => c.User).WithOne(u => u.FidelityCard);

            // correlazione Voucher a ApplicationUser
            builder
                .Entity<Voucher>()
                .HasOne(v => v.User)
                .WithMany(u => u.Vouchers)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // correlazione Coupon a ApplicationUser
            builder
                .Entity<Coupon>()
                .HasOne(c => c.User)
                .WithMany(u => u.Coupons)
                .HasForeignKey(c => c.UserId)
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

            builder.HasSequence<int>("OrderNumber_seq").StartsAt(123456).IncrementsBy(1);

            builder
                .Entity<Order>()
                .Property(o => o.OrderNumber)
                .HasDefaultValueSql("NEXT VALUE FOR OrderNumber_seq");

            builder.Entity<Order>().Property(o => o.TotalDiscount).HasDefaultValue(0);

            builder.Entity<Order>().Property(o => o.ShippingPrice).HasDefaultValue(4.99);

            // Tabella Cart
            builder.Entity<Cart>().Property(c => c.CreatedAt).HasDefaultValue(DateTime.UtcNow);

            builder.Entity<Cart>().Property(c => c.UpdatedAt).HasDefaultValue(DateTime.UtcNow);

            // Tabella CartExperience
            builder
                .Entity<CartExperience>()
                .Property(ce => ce.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Entity<CartExperience>().Property(ce => ce.Quantity).HasDefaultValue(1);

            // Tabella Image
            builder.Entity<Models.Image>().Property(i => i.IsCover).HasDefaultValue(false);

            // Tabella Voucher
            builder.Entity<Voucher>().Property(v => v.CreatedAt).HasDefaultValue(DateTime.UtcNow);

            // Tabella Voucher - funzione per assegnare in modo randomico un codice per la proprietà VoucherCode
            builder
                .Entity<Voucher>()
                .Property(v => v.VoucherCode)
                .HasMaxLength(30)
                .HasDefaultValueSql(
                    @"
                CONCAT(
                    SUBSTRING(REPLACE(CONVERT(varchar(50), NEWID()), '-', ''), 1, 8), '-',
                    SUBSTRING(REPLACE(CONVERT(varchar(50), NEWID()), '-', ''), 9, 8), '-',
                    SUBSTRING(REPLACE(CONVERT(varchar(50), NEWID()), '-', ''), 17, 8)
                )"
                )
                .ValueGeneratedOnAdd();

            builder.Entity<Voucher>().HasIndex(v => v.VoucherCode).IsUnique();

            // Tabella Coupon
            builder.Entity<Coupon>().Property(c => c.PercentualSaleAmount).HasDefaultValue(0);

            builder.Entity<Coupon>().Property(c => c.FixedSaleAmount).HasDefaultValue(0);

            builder.Entity<Coupon>().Property(c => c.IsActive).HasDefaultValue(true);

            builder.Entity<Coupon>().Property(c => c.IsUniversal).HasDefaultValue(false);

            builder.Entity<Coupon>().Property(c => c.MinimumAmount).HasDefaultValue(0);

            // Tabella FidelityCard
            // inserire funzione per assegnare un numero di carta casuale
            builder
                .Entity<FidelityCard>()
                .Property(fc => fc.CardNumber)
                .HasMaxLength(12)
                .HasDefaultValueSql(
                    @"
                RIGHT('000000000000' + 
                    CAST(ABS(CAST(CAST(NEWID() AS VARBINARY) AS BIGINT)) AS VARCHAR(20))
                , 12)"
                )
                .ValueGeneratedOnAdd();

            builder.Entity<FidelityCard>().HasIndex(fc => fc.CardNumber).IsUnique();

            builder.Entity<FidelityCard>().Property(c => c.Points).HasDefaultValue(0);

            builder.Entity<FidelityCard>().Property(c => c.AvailablePoints).HasDefaultValue(0);

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

            // inserimento carrelli nella tabella Cart
            builder
                .Entity<Cart>()
                .HasData(
                    new Cart()
                    {
                        CartId = Guid.Parse("ad0b8ebb-3e25-4c9f-a7dd-7e07c3e7ab3f"),
                        UserId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        CreatedAt = DateTime.Parse("09/04/2025 11:00:56"),
                        UpdatedAt = DateTime.Parse("09/04/2025 11:00:56"),
                    },
                    new Cart()
                    {
                        CartId = Guid.Parse("59a9d57e-c339-4a73-8d02-69cc186a5385"),
                        UserId = "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c",
                        CreatedAt = DateTime.Parse("09/04/2025 11:00:56"),
                        UpdatedAt = DateTime.Parse("09/04/2025 11:00:56"),
                    },
                    new Cart()
                    {
                        CartId = Guid.Parse("b64a049a-6d76-4c1c-866c-e0169c92f1d6"),
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        CreatedAt = DateTime.Parse("09/04/2025 11:00:56"),
                        UpdatedAt = DateTime.Parse("09/04/2025 11:00:56"),
                    },
                    new Cart()
                    {
                        //Mario Rossi
                        CartId = Guid.Parse("a32de9e5-58e6-4ae8-8590-204bf8677abf"),
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("19/04/2025 11:00:56"),
                        UpdatedAt = DateTime.Parse("19/04/2025 11:00:56"),
                    },
                    new Cart()
                    {
                        //Luigi Bianchi
                        CartId = Guid.Parse("0b61eb1c-7294-49ea-94a2-f90273f7e5c9"),
                        UserId = "e5675086-e91e-442a-9c22-27d41bee49a4",
                        CreatedAt = DateTime.Parse("19/04/2025 11:00:56"),
                        UpdatedAt = DateTime.Parse("19/04/2025 11:00:56"),
                    }
                );

            // inserimento utenti nella tabella ApplicationUsers
            builder
                .Entity<ApplicationUser>()
                .HasData(
                    new ApplicationUser()
                    {
                        Id = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        FirstName = "Admin",
                        LastName = "User",
                        RegistrationDate = DateTime.Parse("09/04/2025 11:00:56"),
                        Email = "admin@example.com",
                        NormalizedEmail = "ADMIN@EXAMPLE.COM",
                        UserName = "admin@example.com",
                        NormalizedUserName = "ADMIN@EXAMPLE.COM",
                        // adminadmin
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEJ924mp2s2BX/BpdalZ6f2s1qlMl3fxdcEPcaKFV6BxA5frV73oVpuC1V9F4PHCJ2g==",
                        CartId = Guid.Parse("ad0b8ebb-3e25-4c9f-a7dd-7e07c3e7ab3f"),
                        FidelityCardId = Guid.Parse("772e32cc-cdea-4413-8785-09312f52f33d"),
                    },
                    new ApplicationUser()
                    {
                        Id = "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c",
                        FirstName = "Seller",
                        LastName = "User",
                        RegistrationDate = DateTime.Parse("09/04/2025 11:00:56"),
                        Email = "seller@example.com",
                        NormalizedEmail = "SELLER@EXAMPLE.COM",
                        UserName = "seller@example.com",
                        NormalizedUserName = "SELLER@EXAMPLE.COM",
                        // sellerseller
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEJP1xbBcaikPe32EBy3MLTcexMUhKB7jQsEGuRiIlRJOWuiJwUGI/v0s83m7H70okg==",
                        CartId = Guid.Parse("59a9d57e-c339-4a73-8d02-69cc186a5385"),
                        FidelityCardId = Guid.Parse("326ddfe3-754b-4f24-8cd6-1011bc3cc37e"),
                    },
                    new ApplicationUser()
                    {
                        Id = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        FirstName = "User",
                        LastName = "User",
                        RegistrationDate = DateTime.Parse("09/04/2025 11:00:56"),
                        Email = "user@example.com",
                        NormalizedEmail = "USER@EXAMPLE.COM",
                        UserName = "user@example.com",
                        NormalizedUserName = "USER@EXAMPLE.COM",
                        // useruser
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEL6u4Tox47kxNqt9nm4+vRn+SzahthaQ55UejBFFdJvvUNNCfqIWRI246s9wJiZ43A==",
                        CartId = Guid.Parse("b64a049a-6d76-4c1c-866c-e0169c92f1d6"),
                        FidelityCardId = Guid.Parse("12d1edf2-df86-41d9-8594-0b1859e31932"),
                    },
                    new ApplicationUser()
                    {
                        Id = "698c347e-bb57-4cb4-b672-9940647f250d",
                        FirstName = "Mario",
                        LastName = "Rossi",
                        RegistrationDate = DateTime.Parse("19/04/2025 11:00:56"),
                        Email = "mario.rossi@example.com",
                        NormalizedEmail = "MARIO.ROSSI@EXAMPLE.COM",
                        UserName = "mario.rossi@example.com",
                        NormalizedUserName = "MARIO.ROSSI@EXAMPLE.COM",
                        // mariorossi
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEGqAB3rWtm9yNytryjcGs97J9AVY4J6GC/pnd/eL+/lSc8KXctmVoydETBEp6qnKAg==",
                        CartId = Guid.Parse("a32de9e5-58e6-4ae8-8590-204bf8677abf"),
                        FidelityCardId = Guid.Parse("ac983a29-21fe-4d7b-822f-2de328dee367"),
                    },
                    new ApplicationUser()
                    {
                        Id = "e5675086-e91e-442a-9c22-27d41bee49a4",
                        FirstName = "Luigi",
                        LastName = "Bianchi",
                        RegistrationDate = DateTime.Parse("19/04/2025 11:00:56"),
                        Email = "luigi.bianchi@example.com",
                        NormalizedEmail = "LUIGI.BIANCHI@EXAMPLE.COM",
                        UserName = "luigi.bianchi@example.com",
                        NormalizedUserName = "LUIGI.BIANCHI@EXAMPLE.COM",
                        // luigibianchi
                        PasswordHash =
                            "AQAAAAIAAYagAAAAENabfBTfVAnfCT/fg0+WNYZFHUGtBkj2cdOTFH8XkxudV8ZObX5QzlvepD9DwevyLA==",
                        CartId = Guid.Parse("0b61eb1c-7294-49ea-94a2-f90273f7e5c9"),
                        FidelityCardId = Guid.Parse("3f05415d-e413-4430-bdcd-e668d6f7aa83"),
                    }
                );

            // inserimento dati nella tabella FidelityCard
            builder
                .Entity<FidelityCard>()
                .HasData(
                    // Admin User
                    new FidelityCard()
                    {
                        FidelityCardId = Guid.Parse("772e32cc-cdea-4413-8785-09312f52f33d"),
                        CardNumber = "341020401032",
                        Points = 0,
                        AvailablePoints = 0,
                        UserId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                    },
                    // Seller User
                    new FidelityCard()
                    {
                        FidelityCardId = Guid.Parse("326ddfe3-754b-4f24-8cd6-1011bc3cc37e"),
                        CardNumber = "453728123423",
                        Points = 0,
                        AvailablePoints = 0,
                        UserId = "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c",
                    },
                    // User User
                    new FidelityCard()
                    {
                        FidelityCardId = Guid.Parse("12d1edf2-df86-41d9-8594-0b1859e31932"),
                        CardNumber = "454432678900",
                        Points = 445,
                        AvailablePoints = 445,
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                    },
                    // Mario Rossi
                    new FidelityCard()
                    {
                        FidelityCardId = Guid.Parse("ac983a29-21fe-4d7b-822f-2de328dee367"),
                        CardNumber = "123245783911",
                        Points = 1331,
                        AvailablePoints = 1331,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                    },
                    // Luigi Bianchi
                    new FidelityCard()
                    {
                        FidelityCardId = Guid.Parse("3f05415d-e413-4430-bdcd-e668d6f7aa83"),
                        CardNumber = "873524120039",
                        Points = 0,
                        AvailablePoints = 0,
                        UserId = "e5675086-e91e-442a-9c22-27d41bee49a4",
                    }
                );

            // inserimento dati nella tabella Coupones
            builder
                .Entity<Coupon>()
                .HasData(
                    // Admin User
                    new Coupon()
                    {
                        CouponId = Guid.Parse("180392ae-03d1-4e01-8ca6-b67e038aa412"),
                        PercentualSaleAmount = 5,
                        FixedSaleAmount = 0,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = null,
                        Code = "WELCOME5",
                        MinimumAmount = 100,
                        UserId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                    },
                    // Seller User
                    new Coupon()
                    {
                        CouponId = Guid.Parse("71a3db8e-9f8b-48d0-96a8-b6b5d6ff890e"),
                        PercentualSaleAmount = 5,
                        FixedSaleAmount = 0,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = null,
                        Code = "WELCOME5",
                        MinimumAmount = 100,
                        UserId = "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c",
                    },
                    // User User
                    new Coupon()
                    {
                        CouponId = Guid.Parse("e05605f6-a529-4fc4-a196-4c7122c8b1e4"),
                        PercentualSaleAmount = 5,
                        FixedSaleAmount = 0,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = null,
                        Code = "WELCOME5",
                        MinimumAmount = 100,
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                    },
                    new Coupon()
                    {
                        CouponId = Guid.Parse("0d291863-71f0-45a7-862a-bf74f709757a"),
                        PercentualSaleAmount = 0,
                        FixedSaleAmount = 10,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = DateTime.Parse("31/05/2025 23:59:59"),
                        Code = "BONUS10",
                        MinimumAmount = 200,
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                    },
                    // Mario Rossi
                    new Coupon()
                    {
                        CouponId = Guid.Parse("fa1639d8-8d3e-454e-9d91-826a285d1727"),
                        PercentualSaleAmount = 5,
                        FixedSaleAmount = 0,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = null,
                        Code = "WELCOME5",
                        MinimumAmount = 100,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                    },
                    new Coupon()
                    {
                        CouponId = Guid.Parse("87d2e445-1202-4d8a-a0c2-bf55228e4878"),
                        PercentualSaleAmount = 0,
                        FixedSaleAmount = 10,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = DateTime.Parse("31/05/2025 23:59:59"),
                        Code = "BONUS10",
                        MinimumAmount = 200,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                    },
                    new Coupon()
                    {
                        CouponId = Guid.Parse("642d9766-e5b6-4121-b7df-b49a60d612f7"),
                        PercentualSaleAmount = 15,
                        FixedSaleAmount = 0,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = DateTime.Parse("31/05/2025 23:59:59"),
                        Code = "DEMODAY15",
                        MinimumAmount = 250,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                    },
                    // Luigi Bianchi
                    new Coupon()
                    {
                        CouponId = Guid.Parse("812a87bc-3758-4e35-8b7c-6dbf06775950"),
                        PercentualSaleAmount = 5,
                        FixedSaleAmount = 0,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = null,
                        Code = "WELCOME5",
                        MinimumAmount = 100,
                        UserId = "e5675086-e91e-442a-9c22-27d41bee49a4",
                    }
                );

            // inserimento dati nella tabella ApplicationUserRole
            builder
                .Entity<ApplicationUserRole>()
                .HasData(
                    new ApplicationUserRole()
                    {
                        UserRoleId = Guid.Parse("6f44a915-b24b-4034-9e18-0a1775210ef3"),
                        UserId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        RoleId = "8d64359a-fda6-4096-b40d-f1375775244d",
                    },
                    new ApplicationUserRole()
                    {
                        UserRoleId = Guid.Parse("5224a9f4-547b-4300-8788-26d085155b48"),
                        UserId = "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c",
                        RoleId = "1f00a1d7-cbb6-44bf-bdc8-a3608b1284b9",
                    },
                    new ApplicationUserRole()
                    {
                        UserRoleId = Guid.Parse("16cbe3b5-128b-4e00-9fbb-4e691b00280a"),
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        RoleId = "849b8726-44b3-434b-9b18-48a4e8d4e9dd",
                    },
                    new ApplicationUserRole()
                    {
                        UserRoleId = Guid.Parse("4c4992c2-1d6e-48d5-ad2c-eccfae98c53f"),
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        RoleId = "849b8726-44b3-434b-9b18-48a4e8d4e9dd",
                    },
                    new ApplicationUserRole()
                    {
                        UserRoleId = Guid.Parse("24d7ceb7-a7c8-48d8-a0b0-1ad0ce9f0fcf"),
                        UserId = "e5675086-e91e-442a-9c22-27d41bee49a4",
                        RoleId = "849b8726-44b3-434b-9b18-48a4e8d4e9dd",
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

            // inserimento esperienze nella tabella Experiences
            builder
                .Entity<Experience>()
                .HasData(
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                        Title = "Ferrari Driving Experience a Monza",
                        CategoryId = Guid.Parse("6f3a957c-df09-437c-bc37-f069173eabe2"),
                        Duration = "2 ore",
                        Place = "Monza, Lombardia",
                        Price = 399,
                        DescriptionShort = "Guida una Ferrari sul leggendario circuito di Monza",
                        Description =
                            "Vivi l'emozione di guidare una Ferrari F488 GTB sul mitico circuito di Formula 1 di Monza. Dopo un briefing teorico con un pilota professionista, avrai l'opportunità di metterti al volante di questa supercar italiana e percorrere diversi giri sul circuito. Sentirai l'adrenalina scorrere mentre acceleri sui rettilinei e affronti le curve leggendarie come la Parabolica. Un'esperienza che combina lusso, velocità e il fascino intramontabile del marchio Ferrari. Il pacchetto include anche un video ricordo della tua esperienza in pista.",
                        MaxParticipants = 1,
                        Organiser = "Motor Experience",
                        LoadingDate = DateTime.Parse("09/04/2025 11:00:56"),
                        LastEditDate = DateTime.Parse("09/04/2025 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "Tutto il necessario per la guida: casco, tuta e assicurazione. Video ricordo dell'esperienza.",
                        Sale = 0,
                        IsInEvidence = true,
                        IsPopular = false,
                        IsDeleted = false,
                        CoverImage = "372a18e3-7932-4ef5-8471-99ce5f3e098a.jpg",
                        ValidityInMonths = 24,
                    },
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                        Title = "Cucina toscana nella tenuta di un castello",
                        CategoryId = Guid.Parse("5FDFFA0F-A615-43F2-AA15-88BC8DCEC27F"),
                        Duration = "5 ore",
                        Place = "Chianti, Toscana",
                        Price = 150,
                        DescriptionShort =
                            "Impara a cucinare i piatti tradizionali toscani con la guida di uno chef locale",
                        Description =
                            "Questa esperienza culinaria si svolge in un autentico castello toscano circondato da vigneti e uliveti. Sotto la guida di uno chef locale, imparerai a preparare un menu completo di piatti tradizionali toscani utilizzando ingredienti freschi provenienti direttamente dall'orto del castello e da produttori locali. Il corso inizia con una passeggiata nei giardini per raccogliere erbe aromatiche, seguita dalla preparazione di pasta fatta in casa, un secondo a base di carne e un dolce tipico. Al termine della lezione, gusterai i piatti preparati accompagnati dai vini prodotti nella tenuta. Un'esperienza che coinvolge tutti i sensi e ti permette di portare a casa ricette e tecniche autentiche.",
                        MaxParticipants = 2,
                        Organiser = "Sapori d'Italia",
                        LoadingDate = DateTime.Parse("15/03/2023 11:00:56"),
                        LastEditDate = DateTime.Parse("15/03/2023 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.",
                        Sale = 0,
                        IsInEvidence = true,
                        IsPopular = false,
                        IsDeleted = false,
                        CoverImage = "350540d5-80b9-49fa-8fb2-8a58c80d149c.jpg",
                        ValidityInMonths = 12,
                    },
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                        Title = "Trekking sul sentiero degli Dei",
                        CategoryId = Guid.Parse("BDA4EE71-AF9C-46C6-B1BF-95F178773A2F"),
                        Duration = "6 ore",
                        Place = "Costiera Amalfitana, Campania",
                        Price = 65,
                        DescriptionShort =
                            "Esplora il famoso sentiero con viste panoramiche sulla Costiera Amalfitana",
                        Description =
                            "Il Sentiero degli Dei è uno dei percorsi di trekking più affascinanti d'Italia, che collega Agerola a Positano offrendo viste mozzafiato sulla Costiera Amalfitana. Questa escursione guidata ti porterà lungo antiche mulattiere e sentieri di montagna, attraverso terrazzamenti coltivati a limoni e ulivi, macchia mediterranea e boschi. Durante il cammino, potrai ammirare il blu intenso del mar Tirreno, le isole Li Galli e Capri, e i caratteristici villaggi aggrappati alle scogliere. La guida ti racconterà la storia e le leggende locali, e ti indicherà i luoghi migliori per scattare fotografie indimenticabili.",
                        MaxParticipants = 2,
                        Organiser = "Italia Escursioni",
                        LoadingDate = DateTime.Parse("05/04/2023 11:00:56"),
                        LastEditDate = DateTime.Parse("05/04/2023 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.",
                        Sale = 0,
                        IsInEvidence = true,
                        IsPopular = false,
                        IsDeleted = false,
                        CoverImage = "f34f2a25-8e55-4826-8ce4-aca2a2a76c3a.jpg",
                        ValidityInMonths = 6,
                    },
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                        Title = "Volo in mongolfiera al tramonto",
                        CategoryId = Guid.Parse("DA780FCF-074E-4E0C-B0B8-1BD8E0C0FA6F"),
                        Duration = "3 ore",
                        Place = "Siena, Toscana",
                        Price = 249.99M,
                        DescriptionShort =
                            "Goditi un tramonto mozzafiato sorvolando le colline toscane",
                        Description =
                            "Un'esperienza indimenticabile che ti permetterà di ammirare il magnifico paesaggio toscano da una prospettiva unica. Il volo in mongolfiera al tramonto inizia con un brindisi di benvenuto mentre l'equipaggio prepara il pallone. Durante l'ascesa potrai godere della vista spettacolare delle colline dorate dal sole al tramonto, dei vigneti e degli uliveti che caratterizzano questa splendida regione. Il volo dura circa un'ora e si conclude con un brindisi con prosecco locale e uno spuntino con prodotti tipici.",
                        MaxParticipants = 2,
                        Organiser = "Avventure Italiane",
                        LoadingDate = DateTime.Parse("15/06/2023 11:00:56"),
                        LastEditDate = DateTime.Parse("15/06/2023 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.",
                        Sale = 5,
                        IsInEvidence = false,
                        IsPopular = true,
                        IsDeleted = false,
                        CoverImage = "b33390f8-430a-456c-b821-83a8b9406043.jpg",
                        ValidityInMonths = 8,
                    },
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                        Title = "Tour fotografico di Venezia all'alba",
                        CategoryId = Guid.Parse("48733FB8-DEAE-41B2-B0C6-4FAB3C45CF93"),
                        Duration = "3 ore",
                        Place = "Venezia, Veneto",
                        Price = 95,
                        DescriptionShort =
                            "Cattura l amagia di Venezia nelle prime luci del giorno",
                        Description =
                            "Vivi l'esperienza di fotografare Venezia in un momento magico, quando la città è avvolta nella luce dorata dell'alba e le strade sono ancora tranquille, senza turisti. Questo tour fotografico guidato da un fotografo professionista ti porterà nei luoghi più iconici e nei angoli nascosti della Serenissima, offrendoti l'opportunità di catturare immagini uniche. Il tour inizia in Piazza San Marco, quando è ancora deserta, per poi esplorare i canali minori, i ponti caratteristici e i campielli pittoreschi. Durante il percorso, riceverai consigli tecnici personalizzati e suggerimenti creativi per migliorare le tue fotografie. Il tour è adatto sia ai principianti che ai fotografi esperti, con qualsiasi tipo di macchina fotografica, anche smartphone.",
                        MaxParticipants = 2,
                        Organiser = "Venezia Autentica",
                        LoadingDate = DateTime.Parse("12/05/2023 11:00:56"),
                        LastEditDate = DateTime.Parse("12/05/2023 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.",
                        Sale = 0,
                        IsInEvidence = false,
                        IsPopular = true,
                        IsDeleted = false,
                        CoverImage = "280b9c0f-257e-4f05-b2db-84e704fda33d.jpg",
                        ValidityInMonths = 8,
                    },
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                        Title = "Degustazione di vini in cantina sotterranea",
                        CategoryId = Guid.Parse("5FDFFA0F-A615-43F2-AA15-88BC8DCEC27F"),
                        Duration = "2 ore",
                        Place = "Montepulciano, Toscana",
                        Price = 75,
                        DescriptionShort = "Scopri i segreti del vino in una cantina medievale",
                        Description =
                            "Un'esperienza sensoriale completa nel cuore della campagna toscana, in una cantina storica scavata nel tufo. Questa degustazione guidata ti permetterà di conoscere i segreti della vinificazione, dalla coltivazione dell'uva all'invecchiamento in botte. Accompagnato da un sommelier esperto, percorrerai le gallerie sotterranee dove riposano le botti centenarie, mantenute alla temperatura perfetta dal microclima naturale. La degustazione include sei vini pregiati della tenuta, tra cui Chianti Classico, Super Tuscan e Vin Santo, accompagnati da pane toscano, olio extravergine d'oliva di produzione propria, formaggi locali e salumi artigianali. Durante l'esperienza, imparerai le tecniche di degustazione professionale e come abbinare correttamente il vino al cibo.",
                        MaxParticipants = 2,
                        Organiser = "Cantine Toscane",
                        LoadingDate = DateTime.Parse("20/07/2023 11:00:56"),
                        LastEditDate = DateTime.Parse("20/07/2023 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.",
                        Sale = 0,
                        IsInEvidence = false,
                        IsPopular = true,
                        IsDeleted = false,
                        CoverImage = "4949849a-cc7a-4481-9c77-929fdbb71310.jpg",
                        ValidityInMonths = 12,
                    },
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                        Title = "Rafting nelle rapide del fiume Nera",
                        CategoryId = Guid.Parse("6ACCF29D-8D1C-4EDD-B48A-C70251516B99"),
                        Duration = "4 ore",
                        Place = "Scheggino, Umbria",
                        Price = 85,
                        DescriptionShort =
                            "Un'avventura adrenalinica tra le rapide di uno dei fiumi più belli dell'Umbria",
                        Description =
                            "Preparati a vivere un'avventura emozionante nelle acque cristalline del fiume Nera, situato nel cuore dell'Umbria. Questa esperienza di rafting ti permetterà di affrontare rapide di diversa difficoltà sotto la guida di istruttori professionisti. Prima di iniziare, riceverai un briefing completo sulla sicurezza e sulle tecniche di pagaiata. Il percorso si snoda attraverso paesaggi selvaggi e incontaminati, con gole spettacolari e cascate. Durante le pause potrai anche fare il bagno nelle piscine naturali del fiume. L'attività è adatta sia a principianti che a esperti.",
                        MaxParticipants = 1,
                        Organiser = "Avventure Italiane",
                        LoadingDate = DateTime.Parse("10/07/2023 11:00:56"),
                        LastEditDate = DateTime.Parse("10/07/2023 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.",
                        Sale = 5,
                        IsInEvidence = false,
                        IsPopular = true,
                        IsDeleted = false,
                        CoverImage = "a8b9cf9c-4f8e-4e18-8413-bf5de4cb4b3c.jpg",
                        ValidityInMonths = 12,
                    },
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                        Title = "Escursione in e-bike nei borghi del Montefeltro",
                        CategoryId = Guid.Parse("A4049EF8-1E86-48BF-B514-3930469DDCBD"),
                        Duration = "7 ore",
                        Place = "Urbino, Marche",
                        Price = 70,
                        DescriptionShort =
                            "Pedalata assistita tra castelli, abbazie e panorami mozzafiato",
                        Description =
                            "Esplora le colline del Montefeltro, al confine tra Marche, Toscana ed Emilia-Romagna, a bordo di una moderna e-bike che ti permetterà di percorrere distanze importanti senza eccessiva fatica. Questo tour guidato ti porterà alla scoperta di antichi borghi medievali, castelli rinascimentali e abbazie secolari, immersi in un paesaggio che ha ispirato i fondali di molti dipinti di Piero della Francesca. Partendo da Urbino, patrimonio UNESCO e culla del Rinascimento italiano, pedalerai lungo strade secondarie poco trafficate e sentieri panoramici, visitando perle nascoste come San Leo, con la sua imponente fortezza, Pennabilli, con i suoi misteriosi giardini, e Sant'Agata Feltria, famosa per il tartufo. Durante il percorso, sono previste soste per visite culturali e per degustare prodotti tipici locali, come il formaggio di fossa, il prosciutto di Carpegna e i vini del territorio. La guida ti racconterà la storia e le leggende di questi luoghi, svelando curiosità e aneddoti poco conosciuti. L'esperienza è adatta a tutti, grazie all'assistenza elettrica delle biciclette che rende accessibili anche i tratti in salita.",
                        MaxParticipants = 1,
                        Organiser = "Marche Experience",
                        LoadingDate = DateTime.Parse("10/08/2023 11:00:56"),
                        LastEditDate = DateTime.Parse("10/08/2023 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.",
                        Sale = 0,
                        IsInEvidence = false,
                        IsPopular = false,
                        IsDeleted = false,
                        CoverImage = "5f37b647-e33d-440e-88ed-2e0d956f377a.jpg",
                        ValidityInMonths = 12,
                    },
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                        Title = "Percorso benessere in grotta termale",
                        CategoryId = Guid.Parse("B70671A5-3989-4E7C-9CD5-C6343E09FCDE"),
                        Duration = "3 ore",
                        Place = "Saturnia, Toscana",
                        Price = 95,
                        DescriptionShort =
                            "Rilassati nelle acque termali naturali di una grotta millenaria",
                        Description =
                            "Concediti un'esperienza di puro relax nelle acque termali di una grotta naturale, formata nei millenni dall'azione dell'acqua sulla roccia calcarea. Questo percorso benessere si svolge in un'antica struttura termale, rinnovata con moderni comfort ma rispettosa dell'ambiente naturale unico. All'interno della grotta, illuminata con luci soffuse, potrai immergerti in diverse piscine di acqua termale a temperature variabili, da 28 a 38 gradi, ricche di minerali benefici per la pelle e l'apparato muscolo-scheletrico. Il percorso prevede anche saune di vapore termale, idromassaggi naturali e cascate per il massaggio cervicale. Un esperto ti guiderà tra le varie tappe del percorso, consigliandoti i tempi ottimali di permanenza in ciascuna area. L'esperienza include un trattamento di fangoterapia e si conclude con una tisana rilassante nella sala relax panoramica, con vista sul parco termale.",
                        MaxParticipants = 1,
                        Organiser = "Terme Naturali",
                        LoadingDate = DateTime.Parse("22/03/2023 11:00:56"),
                        LastEditDate = DateTime.Parse("22/03/2023 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.",
                        Sale = 0,
                        IsInEvidence = false,
                        IsPopular = false,
                        IsDeleted = false,
                        CoverImage = "44811c06-278a-45e5-8411-717827a59107.jpg",
                        ValidityInMonths = 12,
                    },
                    new Experience()
                    {
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                        Title = "Escursione notturna sull'Etna",
                        CategoryId = Guid.Parse("1652310E-B8F3-43E7-BD9D-287F73F939B5"),
                        Duration = "6 ore",
                        Place = "Catania, Sicilia",
                        Price = 120,
                        DescriptionShort = "Ammira il vulcano attivo sotto il cielo stellato",
                        Description =
                            "Un'esperienza mozzafiato sul vulcano attivo più grande d'Europa. Questa escursione notturna ti porterà a scoprire l'affascinante paesaggio lunare dell'Etna quando il buio avvolge il vulcano e le stelle brillano intense nel cielo siciliano. Accompagnato da guide vulcanologiche esperte, inizierai il percorso al tramonto per raggiungere i punti panoramici dove godere della vista sulla costa e sui paesi etnei che si illuminano con il calare della notte. Durante il cammino su sentieri di lava solidificata, potrai osservare le colate recenti e i crateri secondari, ascoltando il racconto delle eruzioni storiche e dei miti legati a questo vulcano. Al calare completo della notte, con l'aiuto di lampade frontali, raggiungerai un punto di osservazione privilegiato da cui, con un po' di fortuna, potrai vedere il bagliore rossastro dell'attività vulcanica. L'escursione include una sosta per una cena al sacco in un rifugio di montagna, con degustazione di vini dell'Etna.",
                        MaxParticipants = 2,
                        Organiser = "Sicilia Avventure",
                        LoadingDate = DateTime.Parse("10/04/2023 11:00:56"),
                        LastEditDate = DateTime.Parse("10/04/2023 11:00:56"),
                        UserCreatorId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        UserLastModifyId = "3a8073b2-b954-428a-a4b9-6e4b3f5db051",
                        IsFreeCancellable = true,
                        IncludedDescription =
                            "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.",
                        Sale = 0,
                        IsInEvidence = false,
                        IsPopular = false,
                        IsDeleted = false,
                        CoverImage = "cf8e8bf0-59b5-44b2-bb5c-478b18b7f767.jpg",
                        ValidityInMonths = 24,
                    }
                );

            // inserimento dati nella tabella CarryWith
            builder
                .Entity<CarryWith>()
                .HasData(
                    // esperienza 1 (Ferrari Driving Experience a Monza)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("37f66586-377d-48b0-8775-0f3ab4743fad"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("5440ef3c-3a70-441f-a437-8d83c846ab50"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("ed314f33-6a11-4c0e-b570-c7dd3b43017d"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("ddadd752-df1c-43f7-9466-dcb72fbcf262"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("25860663-e017-4e55-91f0-52e45470f4d3"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                    },
                    // esperienza 2 (Cucina toscana nella tenuta di un castello)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("40070abb-16f7-426e-8768-2f77bc7bc753"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("eba64c18-8396-4bcd-b169-e262b1dfa061"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("b6457b26-bef6-47b4-9637-aa49d76c43fd"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("3339d17f-4fd0-4942-b1de-fc2ba54c15fe"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("7f618cc7-2a38-4219-8ca8-84ac0541576e"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                    },
                    // esperienza 3 (Trekking sul sentiero degli Dei)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("44245cfb-c6b8-46d3-a4da-374a4595cbda"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("5ce0813b-3969-48cd-ba31-f649881c2f85"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("b7125708-7e08-4c1b-8bc7-4cb68bf36a1c"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("f7b8e6a9-267a-4cb1-8823-3d691d15a3e4"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("d4ad2834-0a9e-4a25-ab65-67fc7e9a2856"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                    },
                    // esperienza 4 (Volo in mongolfiera al tramonto)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("cebce589-6d9e-4c93-9ffb-769879360a58"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("2d6da007-4afa-4830-86d4-cdf5d7244de6"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("e6c86760-cf4c-41a5-8450-e085d3c407ec"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("5687e531-773b-4855-b3c0-d8124c3f325c"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("4dc78b29-e9b1-4fd0-8965-a59dae5ac784"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                    },
                    // esperienza 5 (Tour fotografico di Venezia all'alba)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("3a73b99c-bc56-426d-aa3b-2668d270a81c"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("36ff9655-7ad0-4002-8e18-33a9b95f1dc5"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("cf9ffc4f-0db2-4957-a51d-2b919d755705"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("e2669eb7-0538-4ce0-9098-6916b04e7c1e"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("82b97673-c6cd-4dd2-8e10-27e93e19fec1"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                    },
                    // esperienza 6 (Degustazione di vini in cantina sotterranea)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("4607945b-e5c3-41d5-9984-0e6e9ebbb740"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("243df698-9bf6-4ec9-bc7f-9cbe3e02398d"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("f4d999e5-74db-44c3-9f55-8296cfd4f6a4"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("f7b79ffb-a746-4794-9dc7-c6981f23b76d"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("4ef3c543-9181-4fb9-8bb2-7433965af376"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                    },
                    // esperienza 7 (Rafting nelle rapide del fiume Nera)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("9f16a4a2-1fbf-4fad-b5a5-2d3318ebb5dc"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("f4237517-e5cf-4b3a-97c5-9fc40b26351a"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("1db350e1-5cb8-4d9f-ac82-1ba56f938047"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("44ca62ed-9961-4d8e-b236-73b3a775227d"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("a7e1b81a-d252-435a-9e50-48b5549e2aff"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                    },
                    // esperienza 8 (Escursione in e-bike nei borghi del Montefeltro)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("090db950-0043-4e93-98ed-6f65927b08d6"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("457ccc48-cca1-4c79-825b-6e6accdef6ba"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("a39149a6-00f8-44ec-a9a6-133560e8e73e"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("fa3469c9-f1a1-4ce0-be74-58aea3289418"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("b47847bd-c782-49fb-a7f8-b9449a2680b0"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                    },
                    // esperienza 9 (Percorso benessere in grotta termale)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("f82cbfbf-d01a-40ca-b175-20366f94fb4a"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("dd784099-5b3e-4231-bc9d-4c45cdc1e354"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("7ba9453c-a316-4fcc-a45b-99b8d4afab90"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("bcd5eacd-61b5-4bb7-986a-ec7ab5a13f69"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("7b4712ec-b86b-42b4-bde9-24b089ac4b77"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                    },
                    // esperienza 10 (Escursione notturna sull'Etna)
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("aaaa9a81-b420-45a9-9164-4640382cca71"),
                        Name = "Abbigliamento comodo",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("7b1a1b63-3584-4849-ba0d-1ccb54fd0fe3"),
                        Name = "Scarpe adatte all'attività",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("a85cef09-4f5f-4787-af53-977737955c1f"),
                        Name = "Documento d'identità",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("f5673096-288f-4a8d-a706-800cb9739930"),
                        Name = "Macchina fotografica (opzionale)",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                    },
                    new CarryWith()
                    {
                        CarryWithId = Guid.Parse("e90eb389-a733-4917-8c57-24bfbd072dc4"),
                        Name = "Acqua",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                    }
                );

            // inserimento dati nella tabella Images
            builder
                .Entity<Models.Image>()
                .HasData(
                    // esperienza 1 (Ferrari Driving Experience a Monza)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("fd1319bd-125b-44d7-b257-9f6b05b23a09"),
                        Url = "372a18e3-7932-4ef5-8471-99ce5f3e098a.jpg",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("1fda0cd4-e54a-47dc-985f-55295e9d0405"),
                        Url = "d709d72d-e919-4e25-90e7-f7174fab8b45.jpg",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("5e3b3401-2668-4ec9-a893-ee6c535ddd78"),
                        Url = "82b24643-7e82-4042-be6b-d1704c537371.jpg",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("f4840eb1-e492-480f-a418-5f30d68cb215"),
                        Url = "69fee451-9f08-4ab1-bcfb-cf7c8068c3a4.jpg",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("eb227c65-5393-4c68-9eab-68406b78a89d"),
                        Url = "f12738bf-a7f0-4dc1-a16e-90c3fc6ea823.jpg",
                        ExperienceId = Guid.Parse("589aca9c-2b07-42d2-8920-c4406e5da977"),
                    },
                    // esperienza 2 (Cucina toscana nella tenuta di un castello)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("4dbf4c05-cfd0-4584-9717-2443b9dbcc38"),
                        Url = "350540d5-80b9-49fa-8fb2-8a58c80d149c.jpg",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("ead880ce-bc68-40ff-982c-639e96f3de15"),
                        Url = "8a18db5b-415d-4c79-ba04-f6c2bd3e534d.jpg",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("7f58809a-c241-4e3e-b841-9327111940d4"),
                        Url = "e10af629-01f0-41f5-b078-fe56db331999.jpg",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("67673f47-35fd-4c14-a026-d7a61a8936ed"),
                        Url = "d5de5dc4-2e9c-4c2b-8284-85039169bca9.jpg",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("92c77996-58e0-44ad-b263-f02f11820a6d"),
                        Url = "54b08176-c485-416f-8950-9c74a5b1feea.jpg",
                        ExperienceId = Guid.Parse("62947bc9-568c-4c34-a8e1-2fb6f05bca61"),
                    },
                    // esperienza 3 (Trekking sul sentiero degli Dei)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("1c95b993-955d-45ce-853b-984a03a441f8"),
                        Url = "f34f2a25-8e55-4826-8ce4-aca2a2a76c3a.jpg",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("220d183a-888b-4974-b8f6-ee506f647338"),
                        Url = "f2a0c878-277c-4ab7-ac8b-aee7dbf4bfa2.jpg",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("bba9ae96-13a7-4e28-abcd-63f33f63a28d"),
                        Url = "afab16dd-fb18-4b62-ba16-bea8c4514d68.jpg",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("f9ae8eaf-069a-47cd-b19e-6b6e8166165e"),
                        Url = "e46dace3-f0ac-4bca-8fd1-b8e25c815472.jpg",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("4bfd8674-47b5-481a-80ca-bc07ab00d627"),
                        Url = "199c2fa1-560a-4a26-b0e3-7e59f5b04e9f.jpg",
                        ExperienceId = Guid.Parse("bb36c355-2c8e-4a45-9be3-151934e2ff4c"),
                    },
                    // esperienza 4 (Volo in mongolfiera al tramonto)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("f90c38c4-5c6f-467c-b2b1-7cc2967735c1"),
                        Url = "b33390f8-430a-456c-b821-83a8b9406043.jpg",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("67d4b845-2959-41cd-ac95-b442a4e32cb5"),
                        Url = "cce26a3b-f6dc-47eb-a624-2bc4df0a7623.jpg",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("e20bf13c-a35a-44f6-ba11-12eca65c0366"),
                        Url = "edaf24b2-0a7b-4a18-b44f-eca6ad470460.jpg",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("9b2c4cb0-59a6-41b4-84ae-0ddeee3f4e9e"),
                        Url = "800cffd3-c4f6-4395-b444-ff09cf93ba01.jpg",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("721063ed-d498-4070-a4e1-940a9f849a14"),
                        Url = "3b6f886f-a15a-45ff-9644-944d0b97eaaa.jpg",
                        ExperienceId = Guid.Parse("8dc3b2f9-850b-42cc-824c-7758112b9370"),
                    },
                    // esperienza 5 (Tour fotografico di Venezia all'alba)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("86359d64-b633-4db1-8c30-04737b55cb36"),
                        Url = "280b9c0f-257e-4f05-b2db-84e704fda33d.jpg",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("73138467-d5af-499a-8624-f9d5de52a54f"),
                        Url = "77264134-6aab-419e-ad5d-3503697c6823.jpg",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("00889ac8-a53d-4d5e-b655-aac991787de0"),
                        Url = "206d26ab-33c5-4e4a-b79c-9271e14909ef.jpg",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("8fae68b9-e4f8-420a-91a8-4fea7c80b7e0"),
                        Url = "b2bbcf89-6fb0-44a5-924e-6271a8f824b6.jpg",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("a06f8821-9e20-4a89-bd19-27db1ac39f11"),
                        Url = "296d02ff-cc6c-4027-81d6-fa75630d9e5b.jpg",
                        ExperienceId = Guid.Parse("cec8f297-d65b-485a-adc3-f015139cd0c2"),
                    },
                    // esperienza 6 (Degustazione di vini in cantina sotterranea)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("25098581-ef31-4bfb-b64b-a4dfaa6b765d"),
                        Url = "4949849a-cc7a-4481-9c77-929fdbb71310.jpg",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("b23f5aef-aa7e-44c9-b38f-29c89fd60831"),
                        Url = "fbb011ba-234d-4b67-9edd-4e5a5f7c103e.jpg",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("a9ceabed-f21c-4f01-9303-26852c15524a"),
                        Url = "91ccd7ae-1cec-4462-bae5-a0f97cc43713.jpg",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("fd56c8cb-ee90-4140-9134-ab759f6b5be5"),
                        Url = "26ba5e64-1634-41a0-a3de-d08ea7e86d9d.jpg",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("93c20292-3ece-4b3a-b5d7-0eeb103f021e"),
                        Url = "61bf3454-723f-44f2-831e-340673d566fd.jpg",
                        ExperienceId = Guid.Parse("6f236570-1625-4190-9a4f-0da2d0639386"),
                    },
                    // esperienza 7 (Rafting nelle rapide del fiume Nera)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("53600cbe-3ae0-4ff2-9d9e-baba24a5a468"),
                        Url = "a8b9cf9c-4f8e-4e18-8413-bf5de4cb4b3c.jpg",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("4e0c61df-cc97-4538-bc2e-b922d1e4e17b"),
                        Url = "e53b1924-4882-4551-bd05-fe72a6a7769e.jpg",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("714a64bf-fae0-40c6-a198-ec84d320a03c"),
                        Url = "60b84064-9f5b-454e-a2ea-d38c46c2f03f.jpg",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("f2dfc283-587d-4fd9-a29b-599868a05bf6"),
                        Url = "906885f5-840b-4410-bb69-84f6b492c876.jpg",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("e83e40ee-9a82-4885-b661-6e1ecb6dadef"),
                        Url = "ae68d8ba-1066-4c99-b1a0-e487da50c0a6.jpg",
                        ExperienceId = Guid.Parse("81c17e89-5bc3-42bb-9897-ddf27d111440"),
                    },
                    // esperienza 8 (Escursione in e-bike nei borghi del Montefeltro)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("124f2af3-8b9a-4963-b272-638cd975e988"),
                        Url = "5f37b647-e33d-440e-88ed-2e0d956f377a.jpg",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("8b13d878-8ebc-452e-bf29-b483f72c6887"),
                        Url = "0ff472a2-8be1-43a5-8671-4bf71150b036.jpg",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("9ea84996-fb02-49ac-8a73-c315e1bc6abd"),
                        Url = "5d1e782c-7e22-4045-92bc-eb53b15164ce.jpg",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("0c665169-d7a3-4781-8ad3-b656ed553183"),
                        Url = "e676f25d-4eb5-4a57-a5ec-93a8b7d0c26c.jpg",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("411e1f6a-7bb8-48bb-9a50-8de52bff30fb"),
                        Url = "ab6653b8-dcd2-4cf3-9402-78a29db6f26c.jpg",
                        ExperienceId = Guid.Parse("ff3ed239-e178-4632-8385-042286991c66"),
                    },
                    // esperienza 9 (Percorso benessere in grotta termale)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("0a39012b-5764-4b46-b674-4f72ea74bacb"),
                        Url = "44811c06-278a-45e5-8411-717827a59107.jpg",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("dac61436-2392-47d6-a679-6f6d1ae94225"),
                        Url = "d8c1f786-4bb3-44df-b5a8-7b806788c246.jpg",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("3f4df348-1950-4ce6-9d8a-3b5803cdef49"),
                        Url = "54b7f0e6-9115-4b2c-b0a9-37b2f049eaad.jpg",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("79f38fb1-39b2-4f72-a08c-16dac303f0ef"),
                        Url = "0785f2aa-1e76-4d82-ada6-1e70c27621e1.jpg",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("0e33f4df-9337-4158-b444-caedb779b555"),
                        Url = "020a0db7-2ca2-472a-bb79-6fe49dbbb328.jpg",
                        ExperienceId = Guid.Parse("0c94ee3c-86f3-4e83-afb2-2a753416227a"),
                    },
                    // esperienza 10 (Escursione notturna sull'Etna)
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("c904526c-50f9-4bc3-9d3f-e5ce2a1d6400"),
                        Url = "cf8e8bf0-59b5-44b2-bb5c-478b18b7f767.jpg",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                        IsCover = true,
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("1d43d597-036f-4c7c-976b-84a49d8a802a"),
                        Url = "87e71440-0f75-4752-9932-89875061ae2a.jpg",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("0f3000ac-68fd-46e3-b0cc-bcd40f2927bc"),
                        Url = "cc614324-72a9-4020-bf14-57806546f28f.jpg",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("e343c782-8671-4eba-b282-d5fd7a86c9ab"),
                        Url = "0a056b0c-8418-4d1a-8a1e-fd5e366aef78.jpg",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                    },
                    new Models.Image()
                    {
                        ImageId = Guid.Parse("ed5a4c3e-d0b2-4f75-a1e4-825ebe7a748d"),
                        Url = "39f57bd8-d2db-4d9e-8674-16d015703285.jpg",
                        ExperienceId = Guid.Parse("e25b1044-5049-4ca9-954c-db76ae235862"),
                    }
                );

            // inserimento dati nella tabella Orders
            builder
                .Entity<Order>()
                .HasData(
                    // Ordini User
                    new Order()
                    {
                        OrderId = Guid.Parse("089b2a7e-4287-4e1c-8928-693a736db304"),
                        OrderNumber = 123450,
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        State = "Completato",
                        SubTotalPrice = 145,
                        TotalDiscount = 0,
                        ShippingPrice = 4.99m,
                        TotalPrice = 149.99m,
                        CreatedAt = DateTime.Parse("09/02/2024 11:00:56"),
                    },
                    new Order()
                    {
                        OrderId = Guid.Parse("cf854aee-04c4-43ff-bb30-445daa75478a"),
                        OrderNumber = 123451,
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        State = "Completato",
                        SubTotalPrice = 300,
                        TotalDiscount = 0,
                        ShippingPrice = 4.99m,
                        TotalPrice = 304.99m,
                        CreatedAt = DateTime.Parse("25/07/2024 11:00:56"),
                    },
                    // Ordini Mario Rossi
                    new Order()
                    {
                        OrderId = Guid.Parse("dc2a8bdd-f6cc-4637-9104-639f0e020777"),
                        OrderNumber = 123452,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        State = "Completato",
                        SubTotalPrice = 492.49m,
                        TotalDiscount = 0,
                        ShippingPrice = 4.99m,
                        TotalPrice = 497.48m,
                        CreatedAt = DateTime.Parse("12/01/2025 11:00:56"),
                    },
                    new Order()
                    {
                        OrderId = Guid.Parse("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"),
                        OrderNumber = 123453,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        State = "Completato",
                        SubTotalPrice = 360,
                        TotalDiscount = 0,
                        ShippingPrice = 4.99m,
                        TotalPrice = 364.99m,
                        CreatedAt = DateTime.Parse("23/02/2025 11:00:56"),
                    },
                    new Order()
                    {
                        OrderId = Guid.Parse("d1f55060-cb7a-4c66-b674-adda6099dde5"),
                        OrderNumber = 123454,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        State = "In attesa",
                        SubTotalPrice = 479.75m,
                        TotalDiscount = 0,
                        ShippingPrice = 4.99m,
                        TotalPrice = 484.74m,
                        CreatedAt = DateTime.Parse("28/04/2025 11:00:56"),
                    }
                );

            // inserimento dati nella tabella OrderExperiences
            builder
                .Entity<OrderExperience>()
                .HasData(
                    // primo ordine
                    new OrderExperience()
                    {
                        OrderExperienceId = Guid.Parse("39ea9347-0d41-403a-81c5-baf69a343eb9"),
                        Title = "Escursione in e-bike nei borghi del Montefeltro",
                        UnitPrice = 70,
                        TotalPrice = 70,
                        OrderId = Guid.Parse("089b2a7e-4287-4e1c-8928-693a736db304"),
                        Quantity = 1,
                    },
                    new OrderExperience()
                    {
                        OrderExperienceId = Guid.Parse("5bf549b8-f233-4be8-ba39-57377100149e"),
                        Title = "Degustazione di vini in cantina sotterranea",
                        UnitPrice = 75,
                        TotalPrice = 75,
                        OrderId = Guid.Parse("089b2a7e-4287-4e1c-8928-693a736db304"),
                        Quantity = 1,
                    },
                    // secondo ordine
                    new OrderExperience()
                    {
                        OrderExperienceId = Guid.Parse("99c4650a-29ea-4ae9-8c4c-26d86c4497ca"),
                        Title = "Cucina toscana nella tenuta di un castello",
                        UnitPrice = 150,
                        TotalPrice = 300,
                        OrderId = Guid.Parse("cf854aee-04c4-43ff-bb30-445daa75478a"),
                        Quantity = 2,
                    },
                    // terzo ordine
                    new OrderExperience()
                    {
                        OrderExperienceId = Guid.Parse("78bcc835-6806-4d4e-b6fb-1a8cbe0bc1c1"),
                        Title = "Trekking sul sentiero degli Dei",
                        UnitPrice = 65,
                        TotalPrice = 65,
                        OrderId = Guid.Parse("dc2a8bdd-f6cc-4637-9104-639f0e020777"),
                        Quantity = 1,
                    },
                    new OrderExperience()
                    {
                        OrderExperienceId = Guid.Parse("471f10b0-b759-49c0-b34d-aec032d163f6"),
                        Title = "Percorso benessere in grotta termale",
                        UnitPrice = 95,
                        TotalPrice = 190,
                        OrderId = Guid.Parse("dc2a8bdd-f6cc-4637-9104-639f0e020777"),
                        Quantity = 2,
                    },
                    new OrderExperience()
                    {
                        OrderExperienceId = Guid.Parse("f1b25b3f-60b1-4103-a342-76ef3346f1ed"),
                        Title = "Volo in mongolfiera al tramonto",
                        UnitPrice = 237.49m,
                        TotalPrice = 237.49m,
                        OrderId = Guid.Parse("dc2a8bdd-f6cc-4637-9104-639f0e020777"),
                        Quantity = 1,
                    },
                    // quarto ordine
                    new OrderExperience()
                    {
                        OrderExperienceId = Guid.Parse("30f7aa04-aa62-41bc-99a7-9729a455d0a8"),
                        Title = "Escursione notturna sull'Etna",
                        UnitPrice = 120,
                        TotalPrice = 360,
                        OrderId = Guid.Parse("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"),
                        Quantity = 3,
                    },
                    // quinto ordine
                    new OrderExperience()
                    {
                        OrderExperienceId = Guid.Parse("f9bf51b8-0db6-4f5a-86e4-454e4bba6634"),
                        Title = "Rafting nelle rapide del fiume Nera",
                        UnitPrice = 80.75m,
                        TotalPrice = 80.75m,
                        OrderId = Guid.Parse("d1f55060-cb7a-4c66-b674-adda6099dde5"),
                        Quantity = 1,
                    },
                    new OrderExperience()
                    {
                        OrderExperienceId = Guid.Parse("ecc02f40-aeab-4b84-b4e9-308da99eaf22"),
                        Title = "Ferrari Driving Experience a Monza",
                        UnitPrice = 399,
                        TotalPrice = 399,
                        OrderId = Guid.Parse("d1f55060-cb7a-4c66-b674-adda6099dde5"),
                        Quantity = 1,
                    }
                );

            // inserimento dati nella tabella Vouchers
            builder
                .Entity<Voucher>()
                .HasData(
                    // primo ordine
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("fef8e669-b3b7-4ca7-90fd-270465381881"),
                        Title = "Escursione in e-bike nei borghi del Montefeltro",
                        CategoryId = Guid.Parse("A4049EF8-1E86-48BF-B514-3930469DDCBD"),
                        Duration = "7 ore",
                        Place = "Urbino, Marche",
                        Price = 70,
                        Organiser = "Marche Experience",
                        IsFreeCancellable = true,
                        VoucherCode = "34FR4R5T-FR56TY5I-DRIUE321",
                        ReservationDate = DateTime.Parse("28/01/2025 11:00:56"),
                        IsUsed = true,
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        CreatedAt = DateTime.Parse("09/02/2024 11:00:56"),
                        ExpirationDate = DateTime.Parse("09/02/2025 11:00:56"),
                        OrderId = Guid.Parse("089b2a7e-4287-4e1c-8928-693a736db304"),
                    },
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("af5e7042-c9c7-4535-b3f6-d1956eb25441"),
                        Title = "Degustazione di vini in cantina sotterranea",
                        CategoryId = Guid.Parse("5FDFFA0F-A615-43F2-AA15-88BC8DCEC27F"),
                        Duration = "2 ore",
                        Place = "Montepulciano, Toscana",
                        Price = 75,
                        Organiser = "Cantine Toscane",
                        IsFreeCancellable = true,
                        VoucherCode = "ER57HF75-RTIP39E8-WE3210PQ",
                        ReservationDate = DateTime.Parse("17/04/2024 11:00:56"),
                        IsUsed = true,
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        CreatedAt = DateTime.Parse("09/02/2024 11:00:56"),
                        ExpirationDate = DateTime.Parse("09/02/2025 11:00:56"),
                        OrderId = Guid.Parse("089b2a7e-4287-4e1c-8928-693a736db304"),
                    },
                    // secondo ordine
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("e6ebc89b-fefb-43ab-a8e0-df2a48fbe369"),
                        Title = "Cucina toscana nella tenuta di un castello",
                        CategoryId = Guid.Parse("5FDFFA0F-A615-43F2-AA15-88BC8DCEC27F"),
                        Duration = "5 ore",
                        Place = "Chianti, Toscana",
                        Price = 150,
                        Organiser = "Sapori d'Italia",
                        IsFreeCancellable = true,
                        VoucherCode = "QW12EDCM-SX2091QS-1AZWE937",
                        ReservationDate = DateTime.Parse("28/04/2025 11:00:56"),
                        IsUsed = true,
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        CreatedAt = DateTime.Parse("25/07/2024 11:00:56"),
                        ExpirationDate = DateTime.Parse("25/07/2025 11:00:56"),
                        OrderId = Guid.Parse("cf854aee-04c4-43ff-bb30-445daa75478a"),
                    },
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("f1dfe658-59e3-4917-adf1-99a7aae42cd1"),
                        Title = "Cucina toscana nella tenuta di un castello",
                        CategoryId = Guid.Parse("5FDFFA0F-A615-43F2-AA15-88BC8DCEC27F"),
                        Duration = "5 ore",
                        Place = "Chianti, Toscana",
                        Price = 150,
                        Organiser = "Sapori d'Italia",
                        IsFreeCancellable = true,
                        VoucherCode = "SZMAPQ92-AXQW1200-QASW34FR",
                        ReservationDate = DateTime.Parse("28/04/2025 11:00:56"),
                        IsUsed = true,
                        UserId = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        CreatedAt = DateTime.Parse("25/07/2024 11:00:56"),
                        ExpirationDate = DateTime.Parse("25/07/2025 11:00:56"),
                        OrderId = Guid.Parse("cf854aee-04c4-43ff-bb30-445daa75478a"),
                    },
                    // terzo ordine
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("5885d06b-8a41-4e7d-994a-9c87a7133a53"),
                        Title = "Trekking sul sentiero degli Dei",
                        CategoryId = Guid.Parse("BDA4EE71-AF9C-46C6-B1BF-95F178773A2F"),
                        Duration = "6 ore",
                        Place = "Costiera Amalfitana, Campania",
                        Price = 65,
                        Organiser = "Italia Escursioni",
                        IsFreeCancellable = true,
                        VoucherCode = "POET3512-DCF456WR-ASVCTE76",
                        ReservationDate = DateTime.Parse("12/04/2025 11:00:56"),
                        IsUsed = true,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("12/01/2025 11:00:56"),
                        ExpirationDate = DateTime.Parse("12/07/2025 11:00:56"),
                        OrderId = Guid.Parse("dc2a8bdd-f6cc-4637-9104-639f0e020777"),
                    },
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("0931cdc6-0031-40dd-ac09-6a6d6f18a744"),
                        Title = "Percorso benessere in grotta termale",
                        CategoryId = Guid.Parse("B70671A5-3989-4E7C-9CD5-C6343E09FCDE"),
                        Duration = "3 ore",
                        Place = "Saturnia, Toscana",
                        Price = 95,
                        Organiser = "Terme Naturali",
                        IsFreeCancellable = true,
                        VoucherCode = "234DJD37-CMZNXS23-SDER456P",
                        ReservationDate = DateTime.Parse("28/08/2025 11:00:56"),
                        IsUsed = true,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("12/01/2025 11:00:56"),
                        ExpirationDate = DateTime.Parse("12/01/2026 11:00:56"),
                        OrderId = Guid.Parse("dc2a8bdd-f6cc-4637-9104-639f0e020777"),
                    },
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("44f90443-8008-4cb5-87a6-074388ad37a6"),
                        Title = "Percorso benessere in grotta termale",
                        CategoryId = Guid.Parse("B70671A5-3989-4E7C-9CD5-C6343E09FCDE"),
                        Duration = "3 ore",
                        Place = "Saturnia, Toscana",
                        Price = 95,
                        Organiser = "Terme Naturali",
                        IsFreeCancellable = true,
                        VoucherCode = "A10CVD32-XCMZ12WE-CDC34509",
                        ReservationDate = DateTime.Parse("28/08/2025 11:00:56"),
                        IsUsed = true,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("12/01/2025 11:00:56"),
                        ExpirationDate = DateTime.Parse("12/01/2026 11:00:56"),
                        OrderId = Guid.Parse("dc2a8bdd-f6cc-4637-9104-639f0e020777"),
                    },
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("70168df0-0dd4-45db-933a-5edd4356865e"),
                        Title = "Volo in mongolfiera al tramonto",
                        CategoryId = Guid.Parse("DA780FCF-074E-4E0C-B0B8-1BD8E0C0FA6F"),
                        Duration = "3 ore",
                        Place = "Siena, Toscana",
                        Price = 249.99m,
                        Organiser = "Avventure Italiane",
                        IsFreeCancellable = true,
                        VoucherCode = "123POERT-45RT653M-CMPR503I",
                        ReservationDate = DateTime.Parse("08/08/2025 11:00:56"),
                        IsUsed = true,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("12/01/2025 11:00:56"),
                        ExpirationDate = DateTime.Parse("12/09/2025 11:00:56"),
                        OrderId = Guid.Parse("dc2a8bdd-f6cc-4637-9104-639f0e020777"),
                    },
                    // quarto ordine
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("c5a9d186-ec49-4568-9a87-a5832ce2957f"),
                        Title = "Escursione notturna sull'Etna",
                        CategoryId = Guid.Parse("1652310E-B8F3-43E7-BD9D-287F73F939B5"),
                        Duration = "6 ore",
                        Place = "Catania, Sicilia",
                        Price = 120,
                        Organiser = "Sicilia Avventure",
                        IsFreeCancellable = true,
                        VoucherCode = "POWE3456-CMZXNCE5-CMV45012",
                        ReservationDate = DateTime.Parse("21/06/2026 11:00:56"),
                        IsUsed = true,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("23/02/2025 11:00:56"),
                        ExpirationDate = DateTime.Parse("23/02/2027 11:00:56"),
                        OrderId = Guid.Parse("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"),
                    },
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("6e12c524-e60d-4feb-8f49-29c9fd71fc21"),
                        Title = "Escursione notturna sull'Etna",
                        CategoryId = Guid.Parse("1652310E-B8F3-43E7-BD9D-287F73F939B5"),
                        Duration = "6 ore",
                        Place = "Catania, Sicilia",
                        Price = 120,
                        Organiser = "Sicilia Avventure",
                        IsFreeCancellable = true,
                        VoucherCode = "SPE46572-CXASWE12-CMNZGHD4",
                        ReservationDate = DateTime.Parse("21/06/2026 11:00:56"),
                        IsUsed = true,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("23/02/2025 11:00:56"),
                        ExpirationDate = DateTime.Parse("23/02/2027 11:00:56"),
                        OrderId = Guid.Parse("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"),
                    },
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("292c0ec5-a9a7-4484-8b6a-b42ae494a2c7"),
                        Title = "Escursione notturna sull'Etna",
                        CategoryId = Guid.Parse("1652310E-B8F3-43E7-BD9D-287F73F939B5"),
                        Duration = "6 ore",
                        Place = "Catania, Sicilia",
                        Price = 120,
                        Organiser = "Sicilia Avventure",
                        IsFreeCancellable = true,
                        VoucherCode = "MCNXVAST-234756DF-CGDETQ09",
                        ReservationDate = DateTime.Parse("21/06/2026 11:00:56"),
                        IsUsed = true,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("23/02/2025 11:00:56"),
                        ExpirationDate = DateTime.Parse("23/02/2027 11:00:56"),
                        OrderId = Guid.Parse("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"),
                    },
                    // quinto ordine
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("bbc9cf87-e09b-4e31-9b82-6d7086c199d0"),
                        Title = "Rafting nelle rapide del fiume Nera",
                        CategoryId = Guid.Parse("6ACCF29D-8D1C-4EDD-B48A-C70251516B99"),
                        Duration = "4 ore",
                        Place = "Scheggino, Umbria",
                        Price = 80.75m,
                        Organiser = "Avventure Italiane",
                        IsFreeCancellable = true,
                        VoucherCode = "4563HDGR-CV6S7E34-VBXNAJEI",
                        ReservationDate = DateTime.Parse("23/07/2025 11:00:56"),
                        IsUsed = true,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("28/04/2025 11:00:56"),
                        ExpirationDate = DateTime.Parse("28/04/2026 11:00:56"),
                        OrderId = Guid.Parse("d1f55060-cb7a-4c66-b674-adda6099dde5"),
                    },
                    new Voucher()
                    {
                        VoucherId = Guid.Parse("bb706e8b-7647-431d-ab27-99572fb64415"),
                        Title = "Ferrari Driving Experience a Monza",
                        CategoryId = Guid.Parse("6F3A957C-DF09-437C-BC37-F069173EABE2"),
                        Duration = "2 ore",
                        Place = "Monza, Lombardia",
                        Price = 399,
                        Organiser = "Motor Experience",
                        IsFreeCancellable = true,
                        VoucherCode = "QWE23CDT-VBHGDTWQ-ZSDE125E",
                        ReservationDate = DateTime.Parse("15/07/2026 11:00:56"),
                        IsUsed = true,
                        UserId = "698c347e-bb57-4cb4-b672-9940647f250d",
                        CreatedAt = DateTime.Parse("28/04/2025 11:00:56"),
                        ExpirationDate = DateTime.Parse("28/04/2027 11:00:56"),
                        OrderId = Guid.Parse("d1f55060-cb7a-4c66-b674-adda6099dde5"),
                    }
                );
        }
    }
}
