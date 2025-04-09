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
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 9, 13, 40, 46, 870, DateTimeKind.Utc).AddTicks(9478)),
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 9, 13, 40, 46, 871, DateTimeKind.Utc).AddTicks(8363)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 9, 13, 40, 46, 871, DateTimeKind.Utc).AddTicks(9148))
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 9, 13, 40, 46, 871, DateTimeKind.Utc).AddTicks(6110))
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
                    LoadingDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 9, 13, 40, 46, 871, DateTimeKind.Utc).AddTicks(571)),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 9, 13, 40, 46, 871, DateTimeKind.Utc).AddTicks(1328)),
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 9, 13, 40, 46, 871, DateTimeKind.Utc).AddTicks(9768))
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
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2", 0, new Guid("b64a049a-6d76-4c1c-866c-e0169c92f1d6"), "65c60abb-3263-44ae-8581-90da5866e323", "user@exampe.com", false, "Regular", "User", false, null, "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEL6u4Tox47kxNqt9nm4+vRn+SzahthaQ55UejBFFdJvvUNNCfqIWRI246s9wJiZ43A==", null, false, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "05dfeb0b-d6a7-436e-9d27-5055a505733f", false, "user@exampe.com" },
                    { "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 0, new Guid("ad0b8ebb-3e25-4c9f-a7dd-7e07c3e7ab3f"), "0355e64a-42e1-4bb2-bbe7-2d14b10498c5", "admin@exampe.com", false, "Admin", "User", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJ924mp2s2BX/BpdalZ6f2s1qlMl3fxdcEPcaKFV6BxA5frV73oVpuC1V9F4PHCJ2g==", null, false, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "903f7ec9-c24b-4992-a6c6-41b7b3f72a2c", false, "admin@exampe.com" },
                    { "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 0, new Guid("59a9d57e-c339-4a73-8d02-69cc186a5385"), "48cfae98-6a37-4d28-a95f-13a7917688e2", "seller@exampe.com", false, "Seller", "User", false, null, "SELLER@EXAMPLE.COM", "SELLER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJP1xbBcaikPe32EBy3MLTcexMUhKB7jQsEGuRiIlRJOWuiJwUGI/v0s83m7H70okg==", null, false, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "8a7ca3d2-5d72-4ad4-8fe0-166db6d2a1ff", false, "seller@exampe.com" }
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
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsInEvidence", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[,]
                {
                    { new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), new Guid("6f3a957c-df09-437c-bc37-f069173eabe2"), "ferrari.jpg", "Vivi l'emozione di guidare una Ferrari F488 GTB sul mitico circuito di Formula 1 di Monza. Dopo un briefing teorico con un pilota professionista, avrai l'opportunità di metterti al volante di questa supercar italiana e percorrere diversi giri sul circuito. Sentirai l'adrenalina scorrere mentre acceleri sui rettilinei e affronti le curve leggendarie come la Parabolica. Un'esperienza che combina lusso, velocità e il fascino intramontabile del marchio Ferrari. Il pacchetto include anche un video ricordo della tua esperienza in pista.", "Guida una Ferrari sul leggendario circuito di Monza", "2 ore", "Tutto il necessario per la guida: casco, tuta e assicurazione. Video ricordo dell'esperienza.", true, true, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Motor Experience", "Monza, Lombardia", 399m, "Ferrari Driving Experience a Monza", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 24 },
                    { new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), "cucinaToscana.jpg", "Questa esperienza culinaria si svolge in un autentico castello toscano circondato da vigneti e uliveti. Sotto la guida di uno chef locale, imparerai a preparare un menu completo di piatti tradizionali toscani utilizzando ingredienti freschi provenienti direttamente dall'orto del castello e da produttori locali. Il corso inizia con una passeggiata nei giardini per raccogliere erbe aromatiche, seguita dalla preparazione di pasta fatta in casa, un secondo a base di carne e un dolce tipico. Al termine della lezione, gusterai i piatti preparati accompagnati dai vini prodotti nella tenuta. Un'esperienza che coinvolge tutti i sensi e ti permette di portare a casa ricette e tecniche autentiche.", "Impara a cucinare i piatti tradizionali toscani con la guida di uno chef locale", "5 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 3, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Sapori d'Italia", "Chianti, Toscana", 150m, "Cucina toscana nella tenuta di un castello", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 },
                    { new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), new Guid("bda4ee71-af9c-46c6-b1bf-95f178773a2f"), "sentieroDegliDei.jpg", "Il Sentiero degli Dei è uno dei percorsi di trekking più affascinanti d'Italia, che collega Agerola a Positano offrendo viste mozzafiato sulla Costiera Amalfitana. Questa escursione guidata ti porterà lungo antiche mulattiere e sentieri di montagna, attraverso terrazzamenti coltivati a limoni e ulivi, macchia mediterranea e boschi. Durante il cammino, potrai ammirare il blu intenso del mar Tirreno, le isole Li Galli e Capri, e i caratteristici villaggi aggrappati alle scogliere. La guida ti racconterà la storia e le leggende locali, e ti indicherà i luoghi migliori per scattare fotografie indimenticabili.", "Esplora il famoso sentiero con viste panoramiche sulla Costiera Amalfitana", "6 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 4, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Italia Escursioni", "Costiera Amalfitana, Campania", 65m, "Trekking sul sentiero degli Dei", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 6 }
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
