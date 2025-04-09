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
                        Email = "admin@exampe.com",
                        NormalizedEmail = "ADMIN@EXAMPLE.COM",
                        UserName = "admin@exampe.com",
                        NormalizedUserName = "ADMIN@EXAMPLE.COM",
                        // adminadmin
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEJ924mp2s2BX/BpdalZ6f2s1qlMl3fxdcEPcaKFV6BxA5frV73oVpuC1V9F4PHCJ2g==",
                        CartId = Guid.Parse("ad0b8ebb-3e25-4c9f-a7dd-7e07c3e7ab3f"),
                    },
                    new ApplicationUser()
                    {
                        Id = "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c",
                        FirstName = "Seller",
                        LastName = "User",
                        RegistrationDate = DateTime.Parse("09/04/2025 11:00:56"),
                        Email = "seller@exampe.com",
                        NormalizedEmail = "SELLER@EXAMPLE.COM",
                        UserName = "seller@exampe.com",
                        NormalizedUserName = "SELLER@EXAMPLE.COM",
                        // sellerseller
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEJP1xbBcaikPe32EBy3MLTcexMUhKB7jQsEGuRiIlRJOWuiJwUGI/v0s83m7H70okg==",
                        CartId = Guid.Parse("59a9d57e-c339-4a73-8d02-69cc186a5385"),
                    },
                    new ApplicationUser()
                    {
                        Id = "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2",
                        FirstName = "Regular",
                        LastName = "User",
                        RegistrationDate = DateTime.Parse("09/04/2025 11:00:56"),
                        Email = "user@exampe.com",
                        NormalizedEmail = "USER@EXAMPLE.COM",
                        UserName = "user@exampe.com",
                        NormalizedUserName = "USER@EXAMPLE.COM",
                        // useruser
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEL6u4Tox47kxNqt9nm4+vRn+SzahthaQ55UejBFFdJvvUNNCfqIWRI246s9wJiZ43A==",
                        CartId = Guid.Parse("b64a049a-6d76-4c1c-866c-e0169c92f1d6"),
                    }
                );

            //inserimento dati nella tabella ApplicationUserRole
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

            // inserimento esperienze nella tabella esperienze
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
                        CoverImage = "ferrari.jpg",
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
                        CoverImage = "cucinaToscana.jpg",
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
                        CoverImage = "sentieroDegliDei.jpg",
                        ValidityInMonths = 6,
                    }
                );
        }
    }
}
