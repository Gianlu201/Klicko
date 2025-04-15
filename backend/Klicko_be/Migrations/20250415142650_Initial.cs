using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Klicko_be.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "OrderNumber_seq",
                startValue: 100000000L);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 15, 14, 26, 49, 974, DateTimeKind.Utc).AddTicks(8856)),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 15, 14, 26, 49, 975, DateTimeKind.Utc).AddTicks(3682)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 15, 14, 26, 49, 975, DateTimeKind.Utc).AddTicks(4104))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR OrderNumber_seq"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 15, 14, 26, 49, 975, DateTimeKind.Utc).AddTicks(2529))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescriptionShort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    Organiser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadingDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 15, 14, 26, 49, 974, DateTimeKind.Utc).AddTicks(9305)),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 15, 14, 26, 49, 975, DateTimeKind.Utc).AddTicks(26)),
                    UserCreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserLastModifyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsFreeCancellable = table.Column<bool>(type: "bit", nullable: false),
                    IncludedDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sale = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsInEvidence = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidityInMonths = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_Experiences_AspNetUsers_UserCreatorId",
                        column: x => x.UserCreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Experiences_AspNetUsers_UserLastModifyId",
                        column: x => x.UserLastModifyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Experiences_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarryWiths",
                columns: table => new
                {
                    CarryWithId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarryWiths", x => x.CarryWithId);
                    table.ForeignKey(
                        name: "FK_CarryWiths_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartExperiences",
                columns: table => new
                {
                    CartExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 15, 14, 26, 49, 975, DateTimeKind.Utc).AddTicks(4452))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartExperiences", x => x.CartExperienceId);
                    table.ForeignKey(
                        name: "FK_CartExperiences_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId");
                    table.ForeignKey(
                        name: "FK_CartExperiences_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceId");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderExperiences",
                columns: table => new
                {
                    OrderExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderExperiences", x => x.OrderExperienceId);
                    table.ForeignKey(
                        name: "FK_OrderExperiences_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderExperiences_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f00a1d7-cbb6-44bf-bdc8-a3608b1284b9", null, "Seller", "SELLER" },
                    { "849b8726-44b3-434b-9b18-48a4e8d4e9dd", null, "User", "USER" },
                    { "8d64359a-fda6-4096-b40d-f1375775244d", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegistrationDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2", 0, new Guid("b64a049a-6d76-4c1c-866c-e0169c92f1d6"), "d9acd8e2-66e8-4869-bedd-1f657cd2dfad", "user@example.com", false, "Regular", "User", false, null, "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEL6u4Tox47kxNqt9nm4+vRn+SzahthaQ55UejBFFdJvvUNNCfqIWRI246s9wJiZ43A==", null, false, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "3c597795-9e36-4797-8534-6ddf94064bb4", false, "user@example.com" },
                    { "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 0, new Guid("ad0b8ebb-3e25-4c9f-a7dd-7e07c3e7ab3f"), "3f55bd75-e2b3-40a4-9e93-975344af2937", "admin@exampe.com", false, "Admin", "User", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJ924mp2s2BX/BpdalZ6f2s1qlMl3fxdcEPcaKFV6BxA5frV73oVpuC1V9F4PHCJ2g==", null, false, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "6c11c51d-ef5c-43c6-984b-5d3e8d46e212", false, "admin@exampe.com" },
                    { "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 0, new Guid("59a9d57e-c339-4a73-8d02-69cc186a5385"), "003b983f-ea4f-47c6-a4f2-9f29bc4e1365", "seller@exampe.com", false, "Seller", "User", false, null, "SELLER@EXAMPLE.COM", "SELLER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJP1xbBcaikPe32EBy3MLTcexMUhKB7jQsEGuRiIlRJOWuiJwUGI/v0s83m7H70okg==", null, false, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "00e2b372-2b7e-4648-898a-7ecf0ae1f4c4", false, "seller@exampe.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Icon", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("1652310e-b8f3-43e7-bd9d-287f73f939b5"), "Avventura", "avventura.png", "avventura.jpg", "Avventura" },
                    { new Guid("48733fb8-deae-41b2-b0c6-4fab3c45cf93"), "Arte e Cultura", "arteCultura.png", "arteCultura.jpg", "Arte e Cultura" },
                    { new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), "Gastronomia", "Gastronomia.png", "Gastronomia.jpg", "Gastronomia" },
                    { new Guid("6accf29d-8d1c-4edd-b48a-c70251516b99"), "Acqua", "acqua.png", "acqua.jpg", "Acqua" },
                    { new Guid("6f3a957c-df09-437c-bc37-f069173eabe2"), "Motori", "motori.png", "motori.jpg", "Motori" },
                    { new Guid("7f13b386-b8af-4ed1-b42b-845e17f657c3"), "Città", "citta.png", "citta.jpg", "Città" },
                    { new Guid("a4049ef8-1e86-48bf-b514-3930469ddcbd"), "Sport", "sport.png", "sport.jpg", "Sport" },
                    { new Guid("b70671a5-3989-4e7c-9cd5-c6343e09fcde"), "Relax", "relax.png", "relax.jpg", "Relax" },
                    { new Guid("bda4ee71-af9c-46c6-b1bf-95f178773a2f"), "Trekking", "trekking.png", "trekking.jpg", "Trekking" },
                    { new Guid("da780fcf-074e-4e0c-b0b8-1bd8e0c0fa6f"), "Aria", "aria.png", "aria.jpg", "Aria" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "UserRoleId" },
                values: new object[,]
                {
                    { "849b8726-44b3-434b-9b18-48a4e8d4e9dd", "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2", new Guid("16cbe3b5-128b-4e00-9fbb-4e691b00280a") },
                    { "8d64359a-fda6-4096-b40d-f1375775244d", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", new Guid("6f44a915-b24b-4034-9e18-0a1775210ef3") },
                    { "1f00a1d7-cbb6-44bf-bdc8-a3608b1284b9", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", new Guid("5224a9f4-547b-4300-8788-26d085155b48") }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("59a9d57e-c339-4a73-8d02-69cc186a5385"), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c" },
                    { new Guid("ad0b8ebb-3e25-4c9f-a7dd-7e07c3e7ab3f"), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "3a8073b2-b954-428a-a4b9-6e4b3f5db051" },
                    { new Guid("b64a049a-6d76-4c1c-866c-e0169c92f1d6"), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2" }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), new Guid("b70671a5-3989-4e7c-9cd5-c6343e09fcde"), "termeSaturnia.jpg", "Concediti un'esperienza di puro relax nelle acque termali di una grotta naturale, formata nei millenni dall'azione dell'acqua sulla roccia calcarea. Questo percorso benessere si svolge in un'antica struttura termale, rinnovata con moderni comfort ma rispettosa dell'ambiente naturale unico. All'interno della grotta, illuminata con luci soffuse, potrai immergerti in diverse piscine di acqua termale a temperature variabili, da 28 a 38 gradi, ricche di minerali benefici per la pelle e l'apparato muscolo-scheletrico. Il percorso prevede anche saune di vapore termale, idromassaggi naturali e cascate per il massaggio cervicale. Un esperto ti guiderà tra le varie tappe del percorso, consigliandoti i tempi ottimali di permanenza in ciascuna area. L'esperienza include un trattamento di fangoterapia e si conclude con una tisana rilassante nella sala relax panoramica, con vista sul parco termale.", "Rilassati nelle acque termali naturali di una grotta millenaria", "3 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 3, 22, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 22, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Terme Naturali", "Saturnia, Toscana", 95m, "Percorso benessere in grotta termale", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsInEvidence", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[,]
                {
                    { new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), new Guid("6f3a957c-df09-437c-bc37-f069173eabe2"), "ferrari.jpg", "Vivi l'emozione di guidare una Ferrari F488 GTB sul mitico circuito di Formula 1 di Monza. Dopo un briefing teorico con un pilota professionista, avrai l'opportunità di metterti al volante di questa supercar italiana e percorrere diversi giri sul circuito. Sentirai l'adrenalina scorrere mentre acceleri sui rettilinei e affronti le curve leggendarie come la Parabolica. Un'esperienza che combina lusso, velocità e il fascino intramontabile del marchio Ferrari. Il pacchetto include anche un video ricordo della tua esperienza in pista.", "Guida una Ferrari sul leggendario circuito di Monza", "2 ore", "Tutto il necessario per la guida: casco, tuta e assicurazione. Video ricordo dell'esperienza.", true, true, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Motor Experience", "Monza, Lombardia", 399m, "Ferrari Driving Experience a Monza", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 24 },
                    { new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), "cucinaToscana.jpg", "Questa esperienza culinaria si svolge in un autentico castello toscano circondato da vigneti e uliveti. Sotto la guida di uno chef locale, imparerai a preparare un menu completo di piatti tradizionali toscani utilizzando ingredienti freschi provenienti direttamente dall'orto del castello e da produttori locali. Il corso inizia con una passeggiata nei giardini per raccogliere erbe aromatiche, seguita dalla preparazione di pasta fatta in casa, un secondo a base di carne e un dolce tipico. Al termine della lezione, gusterai i piatti preparati accompagnati dai vini prodotti nella tenuta. Un'esperienza che coinvolge tutti i sensi e ti permette di portare a casa ricette e tecniche autentiche.", "Impara a cucinare i piatti tradizionali toscani con la guida di uno chef locale", "5 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 3, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Sapori d'Italia", "Chianti, Toscana", 150m, "Cucina toscana nella tenuta di un castello", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsPopular", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), "degustazioneMontepulciano.jpg", "Un'esperienza sensoriale completa nel cuore della campagna toscana, in una cantina storica scavata nel tufo. Questa degustazione guidata ti permetterà di conoscere i segreti della vinificazione, dalla coltivazione dell'uva all'invecchiamento in botte. Accompagnato da un sommelier esperto, percorrerai le gallerie sotterranee dove riposano le botti centenarie, mantenute alla temperatura perfetta dal microclima naturale. La degustazione include sei vini pregiati della tenuta, tra cui Chianti Classico, Super Tuscan e Vin Santo, accompagnati da pane toscano, olio extravergine d'oliva di produzione propria, formaggi locali e salumi artigianali. Durante l'esperienza, imparerai le tecniche di degustazione professionale e come abbinare correttamente il vino al cibo.", "Scopri i segreti del vino in una cantina medievale", "2 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 7, 20, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 20, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Cantine Toscane", "Montepulciano, Toscana", 75m, "Degustazione di vini in cantina sotterranea", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsPopular", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Sale", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[,]
                {
                    { new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), new Guid("6accf29d-8d1c-4edd-b48a-c70251516b99"), "raftingUmbria.jpg", "Preparati a vivere un'avventura emozionante nelle acque cristalline del fiume Nera, situato nel cuore dell'Umbria. Questa esperienza di rafting ti permetterà di affrontare rapide di diversa difficoltà sotto la guida di istruttori professionisti. Prima di iniziare, riceverai un briefing completo sulla sicurezza e sulle tecniche di pagaiata. Il percorso si snoda attraverso paesaggi selvaggi e incontaminati, con gole spettacolari e cascate. Durante le pause potrai anche fare il bagno nelle piscine naturali del fiume. L'attività è adatta sia a principianti che a esperti.", "Un'avventura adrenalinica tra le rapide di uno dei fiumi più belli dell'Umbria", "4 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 7, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Avventure Italiane", "Scheggino, Umbria", 85m, 5, "Rafting nelle rapide del fiume Nera", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 },
                    { new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), new Guid("da780fcf-074e-4e0c-b0b8-1bd8e0c0fa6f"), "mongolfieraTramonto.jpg", "Un'esperienza indimenticabile che ti permetterà di ammirare il magnifico paesaggio toscano da una prospettiva unica. Il volo in mongolfiera al tramonto inizia con un brindisi di benvenuto mentre l'equipaggio prepara il pallone. Durante l'ascesa potrai godere della vista spettacolare delle colline dorate dal sole al tramonto, dei vigneti e degli uliveti che caratterizzano questa splendida regione. Il volo dura circa un'ora e si conclude con un brindisi con prosecco locale e uno spuntino con prodotti tipici.", "Goditi un tramonto mozzafiato sorvolando le colline toscane", "3 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 6, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Avventure Italiane", "Siena, Toscana", 249.99m, 5, "Volo in mongolfiera al tramonto", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 8 }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsInEvidence", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), new Guid("bda4ee71-af9c-46c6-b1bf-95f178773a2f"), "sentieroDegliDei.jpg", "Il Sentiero degli Dei è uno dei percorsi di trekking più affascinanti d'Italia, che collega Agerola a Positano offrendo viste mozzafiato sulla Costiera Amalfitana. Questa escursione guidata ti porterà lungo antiche mulattiere e sentieri di montagna, attraverso terrazzamenti coltivati a limoni e ulivi, macchia mediterranea e boschi. Durante il cammino, potrai ammirare il blu intenso del mar Tirreno, le isole Li Galli e Capri, e i caratteristici villaggi aggrappati alle scogliere. La guida ti racconterà la storia e le leggende locali, e ti indicherà i luoghi migliori per scattare fotografie indimenticabili.", "Esplora il famoso sentiero con viste panoramiche sulla Costiera Amalfitana", "6 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 4, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Italia Escursioni", "Costiera Amalfitana, Campania", 65m, "Trekking sul sentiero degli Dei", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 6 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsPopular", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), new Guid("48733fb8-deae-41b2-b0c6-4fab3c45cf93"), "veneziaFotografica.jpg", "Vivi l'esperienza di fotografare Venezia in un momento magico, quando la città è avvolta nella luce dorata dell'alba e le strade sono ancora tranquille, senza turisti. Questo tour fotografico guidato da un fotografo professionista ti porterà nei luoghi più iconici e nei angoli nascosti della Serenissima, offrendoti l'opportunità di catturare immagini uniche. Il tour inizia in Piazza San Marco, quando è ancora deserta, per poi esplorare i canali minori, i ponti caratteristici e i campielli pittoreschi. Durante il percorso, riceverai consigli tecnici personalizzati e suggerimenti creativi per migliorare le tue fotografie. Il tour è adatto sia ai principianti che ai fotografi esperti, con qualsiasi tipo di macchina fotografica, anche smartphone.", "Cattura l amagia di Venezia nelle prime luci del giorno", "3 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 5, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Venezia Autentica", "Venezia, Veneto", 95m, "Tour fotografico di Venezia all'alba", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 8 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[,]
                {
                    { new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), new Guid("1652310e-b8f3-43e7-bd9d-287f73f939b5"), "escursioneEtna.jpg", "Un'esperienza mozzafiato sul vulcano attivo più grande d'Europa. Questa escursione notturna ti porterà a scoprire l'affascinante paesaggio lunare dell'Etna quando il buio avvolge il vulcano e le stelle brillano intense nel cielo siciliano. Accompagnato da guide vulcanologiche esperte, inizierai il percorso al tramonto per raggiungere i punti panoramici dove godere della vista sulla costa e sui paesi etnei che si illuminano con il calare della notte. Durante il cammino su sentieri di lava solidificata, potrai osservare le colate recenti e i crateri secondari, ascoltando il racconto delle eruzioni storiche e dei miti legati a questo vulcano. Al calare completo della notte, con l'aiuto di lampade frontali, raggiungerai un punto di osservazione privilegiato da cui, con un po' di fortuna, potrai vedere il bagliore rossastro dell'attività vulcanica. L'escursione include una sosta per una cena al sacco in un rifugio di montagna, con degustazione di vini dell'Etna.", "Ammira il vulcano attivo sotto il cielo stellato", "6 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 4, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Sicilia Avventure", "Catania, Sicilia", 120m, "Escursione notturna sull'Etna", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 24 },
                    { new Guid("ff3ed239-e178-4632-8385-042286991c66"), new Guid("a4049ef8-1e86-48bf-b514-3930469ddcbd"), "ebikeMontefeltro.jpg", "Esplora le colline del Montefeltro, al confine tra Marche, Toscana ed Emilia-Romagna, a bordo di una moderna e-bike che ti permetterà di percorrere distanze importanti senza eccessiva fatica. Questo tour guidato ti porterà alla scoperta di antichi borghi medievali, castelli rinascimentali e abbazie secolari, immersi in un paesaggio che ha ispirato i fondali di molti dipinti di Piero della Francesca. Partendo da Urbino, patrimonio UNESCO e culla del Rinascimento italiano, pedalerai lungo strade secondarie poco trafficate e sentieri panoramici, visitando perle nascoste come San Leo, con la sua imponente fortezza, Pennabilli, con i suoi misteriosi giardini, e Sant'Agata Feltria, famosa per il tartufo. Durante il percorso, sono previste soste per visite culturali e per degustare prodotti tipici locali, come il formaggio di fossa, il prosciutto di Carpegna e i vini del territorio. La guida ti racconterà la storia e le leggende di questi luoghi, svelando curiosità e aneddoti poco conosciuti. L'esperienza è adatta a tutti, grazie all'assistenza elettrica delle biciclette che rende accessibili anche i tratti in salita.", "Pedalata assistita tra castelli, abbazie e panorami mozzafiato", "7 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 8, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Marche Experience", "Urbino, Marche", 70m, "Escursione in e-bike nei borghi del Montefeltro", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 }
                });

            migrationBuilder.InsertData(
                table: "CarryWiths",
                columns: new[] { "CarryWithId", "ExperienceId", "Name" },
                values: new object[,]
                {
                    { new Guid("090db950-0043-4e93-98ed-6f65927b08d6"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Abbigliamento comodo" },
                    { new Guid("1db350e1-5cb8-4d9f-ac82-1ba56f938047"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Documento d'identità" },
                    { new Guid("243df698-9bf6-4ec9-bc7f-9cbe3e02398d"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Scarpe adatte all'attività" },
                    { new Guid("25860663-e017-4e55-91f0-52e45470f4d3"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Acqua" },
                    { new Guid("2d6da007-4afa-4830-86d4-cdf5d7244de6"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Scarpe adatte all'attività" },
                    { new Guid("3339d17f-4fd0-4942-b1de-fc2ba54c15fe"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Macchina fotografica (opzionale)" },
                    { new Guid("36ff9655-7ad0-4002-8e18-33a9b95f1dc5"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Scarpe adatte all'attività" },
                    { new Guid("37f66586-377d-48b0-8775-0f3ab4743fad"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Abbigliamento comodo" },
                    { new Guid("3a73b99c-bc56-426d-aa3b-2668d270a81c"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Abbigliamento comodo" },
                    { new Guid("40070abb-16f7-426e-8768-2f77bc7bc753"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Abbigliamento comodo" },
                    { new Guid("44245cfb-c6b8-46d3-a4da-374a4595cbda"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Abbigliamento comodo" },
                    { new Guid("44ca62ed-9961-4d8e-b236-73b3a775227d"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Macchina fotografica (opzionale)" },
                    { new Guid("457ccc48-cca1-4c79-825b-6e6accdef6ba"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Scarpe adatte all'attività" },
                    { new Guid("4607945b-e5c3-41d5-9984-0e6e9ebbb740"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Abbigliamento comodo" },
                    { new Guid("4dc78b29-e9b1-4fd0-8965-a59dae5ac784"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Acqua" },
                    { new Guid("4ef3c543-9181-4fb9-8bb2-7433965af376"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Acqua" },
                    { new Guid("5440ef3c-3a70-441f-a437-8d83c846ab50"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Scarpe adatte all'attività" },
                    { new Guid("5687e531-773b-4855-b3c0-d8124c3f325c"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Macchina fotografica (opzionale)" },
                    { new Guid("5ce0813b-3969-48cd-ba31-f649881c2f85"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Scarpe adatte all'attività" },
                    { new Guid("7b1a1b63-3584-4849-ba0d-1ccb54fd0fe3"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Scarpe adatte all'attività" },
                    { new Guid("7b4712ec-b86b-42b4-bde9-24b089ac4b77"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Acqua" },
                    { new Guid("7ba9453c-a316-4fcc-a45b-99b8d4afab90"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Documento d'identità" },
                    { new Guid("7f618cc7-2a38-4219-8ca8-84ac0541576e"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Acqua" },
                    { new Guid("82b97673-c6cd-4dd2-8e10-27e93e19fec1"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Acqua" },
                    { new Guid("9f16a4a2-1fbf-4fad-b5a5-2d3318ebb5dc"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Abbigliamento comodo" },
                    { new Guid("a39149a6-00f8-44ec-a9a6-133560e8e73e"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Documento d'identità" },
                    { new Guid("a7e1b81a-d252-435a-9e50-48b5549e2aff"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Acqua" },
                    { new Guid("a85cef09-4f5f-4787-af53-977737955c1f"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Documento d'identità" },
                    { new Guid("aaaa9a81-b420-45a9-9164-4640382cca71"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Abbigliamento comodo" },
                    { new Guid("b47847bd-c782-49fb-a7f8-b9449a2680b0"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Acqua" },
                    { new Guid("b6457b26-bef6-47b4-9637-aa49d76c43fd"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Documento d'identità" },
                    { new Guid("b7125708-7e08-4c1b-8bc7-4cb68bf36a1c"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Documento d'identità" },
                    { new Guid("bcd5eacd-61b5-4bb7-986a-ec7ab5a13f69"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Macchina fotografica (opzionale)" },
                    { new Guid("cebce589-6d9e-4c93-9ffb-769879360a58"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Abbigliamento comodo" },
                    { new Guid("cf9ffc4f-0db2-4957-a51d-2b919d755705"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Documento d'identità" },
                    { new Guid("d4ad2834-0a9e-4a25-ab65-67fc7e9a2856"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Acqua" },
                    { new Guid("dd784099-5b3e-4231-bc9d-4c45cdc1e354"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Scarpe adatte all'attività" },
                    { new Guid("ddadd752-df1c-43f7-9466-dcb72fbcf262"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Macchina fotografica (opzionale)" },
                    { new Guid("e2669eb7-0538-4ce0-9098-6916b04e7c1e"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Macchina fotografica (opzionale)" },
                    { new Guid("e6c86760-cf4c-41a5-8450-e085d3c407ec"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Documento d'identità" },
                    { new Guid("e90eb389-a733-4917-8c57-24bfbd072dc4"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Acqua" },
                    { new Guid("eba64c18-8396-4bcd-b169-e262b1dfa061"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Scarpe adatte all'attività" },
                    { new Guid("ed314f33-6a11-4c0e-b570-c7dd3b43017d"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Documento d'identità" },
                    { new Guid("f4237517-e5cf-4b3a-97c5-9fc40b26351a"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Scarpe adatte all'attività" },
                    { new Guid("f4d999e5-74db-44c3-9f55-8296cfd4f6a4"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Documento d'identità" },
                    { new Guid("f5673096-288f-4a8d-a706-800cb9739930"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Macchina fotografica (opzionale)" },
                    { new Guid("f7b79ffb-a746-4794-9dc7-c6981f23b76d"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Macchina fotografica (opzionale)" },
                    { new Guid("f7b8e6a9-267a-4cb1-8823-3d691d15a3e4"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Macchina fotografica (opzionale)" },
                    { new Guid("f82cbfbf-d01a-40ca-b175-20366f94fb4a"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Abbigliamento comodo" },
                    { new Guid("fa3469c9-f1a1-4ce0-be74-58aea3289418"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Macchina fotografica (opzionale)" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("00889ac8-a53d-4d5e-b655-aac991787de0"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "exp5img2.jpg" },
                    { new Guid("0c665169-d7a3-4781-8ad3-b656ed553183"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "exp8img3.jpg" },
                    { new Guid("0e33f4df-9337-4158-b444-caedb779b555"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "exp9img4.jpg" },
                    { new Guid("0f3000ac-68fd-46e3-b0cc-bcd40f2927bc"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "exp10img2.jpg" },
                    { new Guid("1d43d597-036f-4c7c-976b-84a49d8a802a"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "exp10img1.jpg" },
                    { new Guid("1fda0cd4-e54a-47dc-985f-55295e9d0405"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "exp1img1.jpg" },
                    { new Guid("220d183a-888b-4974-b8f6-ee506f647338"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "exp3img1.jpg" },
                    { new Guid("3f4df348-1950-4ce6-9d8a-3b5803cdef49"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "exp9img2.jpg" },
                    { new Guid("411e1f6a-7bb8-48bb-9a50-8de52bff30fb"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "exp8img4.jpg" },
                    { new Guid("4bfd8674-47b5-481a-80ca-bc07ab00d627"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "exp3img4.jpg" },
                    { new Guid("4e0c61df-cc97-4538-bc2e-b922d1e4e17b"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "exp7img1.jpg" },
                    { new Guid("5e3b3401-2668-4ec9-a893-ee6c535ddd78"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "exp1img2.jpg" },
                    { new Guid("67673f47-35fd-4c14-a026-d7a61a8936ed"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "exp2img3.jpg" },
                    { new Guid("67d4b845-2959-41cd-ac95-b442a4e32cb5"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "exp4img1.jpg" },
                    { new Guid("714a64bf-fae0-40c6-a198-ec84d320a03c"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "exp7img2.jpg" },
                    { new Guid("721063ed-d498-4070-a4e1-940a9f849a14"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "exp4img4.jpg" },
                    { new Guid("73138467-d5af-499a-8624-f9d5de52a54f"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "exp5img1.jpg" },
                    { new Guid("79f38fb1-39b2-4f72-a08c-16dac303f0ef"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "exp9img3.jpg" },
                    { new Guid("7f58809a-c241-4e3e-b841-9327111940d4"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "exp2img2.jpg" },
                    { new Guid("8b13d878-8ebc-452e-bf29-b483f72c6887"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "exp8img1.jpg" },
                    { new Guid("8fae68b9-e4f8-420a-91a8-4fea7c80b7e0"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "exp5img3.jpg" },
                    { new Guid("92c77996-58e0-44ad-b263-f02f11820a6d"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "exp2img4.jpg" },
                    { new Guid("93c20292-3ece-4b3a-b5d7-0eeb103f021e"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "exp6img4.jpg" },
                    { new Guid("9b2c4cb0-59a6-41b4-84ae-0ddeee3f4e9e"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "exp4img3.jpg" },
                    { new Guid("9ea84996-fb02-49ac-8a73-c315e1bc6abd"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "exp8img2.jpg" },
                    { new Guid("a06f8821-9e20-4a89-bd19-27db1ac39f11"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "exp5img4.jpg" },
                    { new Guid("a9ceabed-f21c-4f01-9303-26852c15524a"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "exp6img2.jpg" },
                    { new Guid("b23f5aef-aa7e-44c9-b38f-29c89fd60831"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "exp6img1.jpg" },
                    { new Guid("bba9ae96-13a7-4e28-abcd-63f33f63a28d"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "exp3img2.jpg" },
                    { new Guid("dac61436-2392-47d6-a679-6f6d1ae94225"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "exp9img1.jpg" },
                    { new Guid("e20bf13c-a35a-44f6-ba11-12eca65c0366"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "exp4img2.jpg" },
                    { new Guid("e343c782-8671-4eba-b282-d5fd7a86c9ab"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "exp10img3.jpg" },
                    { new Guid("e83e40ee-9a82-4885-b661-6e1ecb6dadef"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "exp7img4.jpg" },
                    { new Guid("ead880ce-bc68-40ff-982c-639e96f3de15"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "exp2img1.jpg" },
                    { new Guid("eb227c65-5393-4c68-9eab-68406b78a89d"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "exp1img4.jpg" },
                    { new Guid("ed5a4c3e-d0b2-4f75-a1e4-825ebe7a748d"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "exp10img4.jpg" },
                    { new Guid("f2dfc283-587d-4fd9-a29b-599868a05bf6"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "exp7img3.jpg" },
                    { new Guid("f4840eb1-e492-480f-a418-5f30d68cb215"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "exp1img3.jpg" },
                    { new Guid("f9ae8eaf-069a-47cd-b19e-6b6e8166165e"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "exp3img3.jpg" },
                    { new Guid("fd56c8cb-ee90-4140-9134-ab759f6b5be5"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "exp6img3.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CarryWiths_ExperienceId",
                table: "CarryWiths",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_CartExperiences_CartId",
                table: "CartExperiences",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartExperiences_ExperienceId",
                table: "CartExperiences",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CategoryId",
                table: "Experiences",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserCreatorId",
                table: "Experiences",
                column: "UserCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserLastModifyId",
                table: "Experiences",
                column: "UserLastModifyId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ExperienceId",
                table: "Images",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderExperiences_ExperienceId",
                table: "OrderExperiences",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderExperiences_OrderId",
                table: "OrderExperiences",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarryWiths");

            migrationBuilder.DropTable(
                name: "CartExperiences");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "OrderExperiences");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropSequence(
                name: "OrderNumber_seq");
        }
    }
}
