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
                startValue: 123456L);

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
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 5, 3, 23, 19, 59, 782, DateTimeKind.Utc).AddTicks(4008)),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FidelityCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 5, 3, 23, 19, 59, 783, DateTimeKind.Utc).AddTicks(119)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 5, 3, 23, 19, 59, 783, DateTimeKind.Utc).AddTicks(917))
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
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PercentualSaleAmount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FixedSaleAmount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsUniversal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimumAmount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                    table.ForeignKey(
                        name: "FK_Coupons_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FidelityCards",
                columns: table => new
                {
                    FidelityCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true, defaultValueSql: "\r\n                RIGHT('000000000000' + \r\n                    CAST(ABS(CAST(CAST(NEWID() AS VARBINARY) AS BIGINT)) AS VARCHAR(20))\r\n                , 12)"),
                    Points = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AvailablePoints = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FidelityCards", x => x.FidelityCardId);
                    table.ForeignKey(
                        name: "FK_FidelityCards_AspNetUsers_UserId",
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
                    SubTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 5, 3, 23, 19, 59, 782, DateTimeKind.Utc).AddTicks(7834)),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    LoadingDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 5, 3, 23, 19, 59, 782, DateTimeKind.Utc).AddTicks(4597)),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 5, 3, 23, 19, 59, 782, DateTimeKind.Utc).AddTicks(5150)),
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
                name: "Vouchers",
                columns: table => new
                {
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Organiser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFreeCancellable = table.Column<bool>(type: "bit", nullable: false),
                    VoucherCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, defaultValueSql: "\r\n                CONCAT(\r\n                    SUBSTRING(REPLACE(CONVERT(varchar(50), NEWID()), '-', ''), 1, 8), '-',\r\n                    SUBSTRING(REPLACE(CONVERT(varchar(50), NEWID()), '-', ''), 9, 8), '-',\r\n                    SUBSTRING(REPLACE(CONVERT(varchar(50), NEWID()), '-', ''), 17, 8)\r\n                )"),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 5, 3, 23, 19, 59, 783, DateTimeKind.Utc).AddTicks(2918)),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.VoucherId);
                    table.ForeignKey(
                        name: "FK_Vouchers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vouchers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Vouchers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 5, 3, 23, 19, 59, 783, DateTimeKind.Utc).AddTicks(1652))
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
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCover = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderExperiences", x => x.OrderExperienceId);
                    table.ForeignKey(
                        name: "FK_OrderExperiences_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceId");
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
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FidelityCardId", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegistrationDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2", 0, new Guid("b64a049a-6d76-4c1c-866c-e0169c92f1d6"), "4a2a6619-6add-48ab-89f0-f3f106ebaa99", "user@example.com", false, new Guid("12d1edf2-df86-41d9-8594-0b1859e31932"), "User", "User", false, null, "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEL6u4Tox47kxNqt9nm4+vRn+SzahthaQ55UejBFFdJvvUNNCfqIWRI246s9wJiZ43A==", null, false, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "581e42f1-ad70-4a5d-805a-4fa2af55860a", false, "user@example.com" },
                    { "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 0, new Guid("ad0b8ebb-3e25-4c9f-a7dd-7e07c3e7ab3f"), "509e3836-6764-4a7b-91c0-f31fbd2e0bd0", "admin@example.com", false, new Guid("772e32cc-cdea-4413-8785-09312f52f33d"), "Admin", "User", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJ924mp2s2BX/BpdalZ6f2s1qlMl3fxdcEPcaKFV6BxA5frV73oVpuC1V9F4PHCJ2g==", null, false, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "f205b61b-2072-407f-be82-427c8cfa0802", false, "admin@example.com" },
                    { "698c347e-bb57-4cb4-b672-9940647f250d", 0, new Guid("a32de9e5-58e6-4ae8-8590-204bf8677abf"), "1650e26a-b558-4ab3-b9d4-fde4e81bad5f", "mario.rossi@example.com", false, new Guid("ac983a29-21fe-4d7b-822f-2de328dee367"), "Mario", "Rossi", false, null, "MARIO.ROSSI@EXAMPLE.COM", "MARIO.ROSSI@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGqAB3rWtm9yNytryjcGs97J9AVY4J6GC/pnd/eL+/lSc8KXctmVoydETBEp6qnKAg==", null, false, new DateTime(2025, 4, 19, 11, 0, 56, 0, DateTimeKind.Unspecified), "97a71588-b7c5-4075-9e0f-3916354aefdd", false, "mario.rossi@example.com" },
                    { "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 0, new Guid("59a9d57e-c339-4a73-8d02-69cc186a5385"), "3b4e91b2-2e55-4eee-bb4d-ba2a941de234", "seller@example.com", false, new Guid("326ddfe3-754b-4f24-8cd6-1011bc3cc37e"), "Seller", "User", false, null, "SELLER@EXAMPLE.COM", "SELLER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJP1xbBcaikPe32EBy3MLTcexMUhKB7jQsEGuRiIlRJOWuiJwUGI/v0s83m7H70okg==", null, false, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "85b24eac-eb05-444c-948b-27672303e907", false, "seller@example.com" },
                    { "e5675086-e91e-442a-9c22-27d41bee49a4", 0, new Guid("0b61eb1c-7294-49ea-94a2-f90273f7e5c9"), "2f5819f4-f6ab-4f08-bd9b-22aa4090ed72", "luigi.bianchi@example.com", false, new Guid("3f05415d-e413-4430-bdcd-e668d6f7aa83"), "Luigi", "Bianchi", false, null, "LUIGI.BIANCHI@EXAMPLE.COM", "LUIGI.BIANCHI@EXAMPLE.COM", "AQAAAAIAAYagAAAAENabfBTfVAnfCT/fg0+WNYZFHUGtBkj2cdOTFH8XkxudV8ZObX5QzlvepD9DwevyLA==", null, false, new DateTime(2025, 4, 19, 11, 0, 56, 0, DateTimeKind.Unspecified), "3b7bf41d-17a8-407a-80df-24a498209eaf", false, "luigi.bianchi@example.com" }
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
                    { "849b8726-44b3-434b-9b18-48a4e8d4e9dd", "698c347e-bb57-4cb4-b672-9940647f250d", new Guid("4c4992c2-1d6e-48d5-ad2c-eccfae98c53f") },
                    { "1f00a1d7-cbb6-44bf-bdc8-a3608b1284b9", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", new Guid("5224a9f4-547b-4300-8788-26d085155b48") },
                    { "849b8726-44b3-434b-9b18-48a4e8d4e9dd", "e5675086-e91e-442a-9c22-27d41bee49a4", new Guid("24d7ceb7-a7c8-48d8-a0b0-1ad0ce9f0fcf") }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("0b61eb1c-7294-49ea-94a2-f90273f7e5c9"), new DateTime(2025, 4, 19, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 19, 11, 0, 56, 0, DateTimeKind.Unspecified), "e5675086-e91e-442a-9c22-27d41bee49a4" },
                    { new Guid("59a9d57e-c339-4a73-8d02-69cc186a5385"), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c" },
                    { new Guid("a32de9e5-58e6-4ae8-8590-204bf8677abf"), new DateTime(2025, 4, 19, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 19, 11, 0, 56, 0, DateTimeKind.Unspecified), "698c347e-bb57-4cb4-b672-9940647f250d" },
                    { new Guid("ad0b8ebb-3e25-4c9f-a7dd-7e07c3e7ab3f"), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "3a8073b2-b954-428a-a4b9-6e4b3f5db051" },
                    { new Guid("b64a049a-6d76-4c1c-866c-e0169c92f1d6"), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2" }
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Code", "ExpireDate", "FixedSaleAmount", "IsActive", "MinimumAmount", "UserId" },
                values: new object[] { new Guid("0d291863-71f0-45a7-862a-bf74f709757a"), "BONUS10", new DateTime(2025, 5, 31, 23, 59, 59, 0, DateTimeKind.Unspecified), 10, true, 200, "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2" });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Code", "ExpireDate", "IsActive", "MinimumAmount", "PercentualSaleAmount", "UserId" },
                values: new object[,]
                {
                    { new Guid("180392ae-03d1-4e01-8ca6-b67e038aa412"), "WELCOME5", null, true, 100, 5, "3a8073b2-b954-428a-a4b9-6e4b3f5db051" },
                    { new Guid("642d9766-e5b6-4121-b7df-b49a60d612f7"), "DEMODAY15", new DateTime(2025, 5, 31, 23, 59, 59, 0, DateTimeKind.Unspecified), true, 250, 15, "698c347e-bb57-4cb4-b672-9940647f250d" },
                    { new Guid("71a3db8e-9f8b-48d0-96a8-b6b5d6ff890e"), "WELCOME5", null, true, 100, 5, "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c" },
                    { new Guid("812a87bc-3758-4e35-8b7c-6dbf06775950"), "WELCOME5", null, true, 100, 5, "e5675086-e91e-442a-9c22-27d41bee49a4" }
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Code", "ExpireDate", "FixedSaleAmount", "IsActive", "MinimumAmount", "UserId" },
                values: new object[] { new Guid("87d2e445-1202-4d8a-a0c2-bf55228e4878"), "BONUS10", new DateTime(2025, 5, 31, 23, 59, 59, 0, DateTimeKind.Unspecified), 10, true, 200, "698c347e-bb57-4cb4-b672-9940647f250d" });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Code", "ExpireDate", "IsActive", "MinimumAmount", "PercentualSaleAmount", "UserId" },
                values: new object[,]
                {
                    { new Guid("e05605f6-a529-4fc4-a196-4c7122c8b1e4"), "WELCOME5", null, true, 100, 5, "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2" },
                    { new Guid("fa1639d8-8d3e-454e-9d91-826a285d1727"), "WELCOME5", null, true, 100, 5, "698c347e-bb57-4cb4-b672-9940647f250d" }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[,]
                {
                    { new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), new Guid("b70671a5-3989-4e7c-9cd5-c6343e09fcde"), "44811c06-278a-45e5-8411-717827a59107.jpg", "Concediti un'esperienza di puro relax nelle acque termali di una grotta naturale, formata nei millenni dall'azione dell'acqua sulla roccia calcarea. Questo percorso benessere si svolge in un'antica struttura termale, rinnovata con moderni comfort ma rispettosa dell'ambiente naturale unico. All'interno della grotta, illuminata con luci soffuse, potrai immergerti in diverse piscine di acqua termale a temperature variabili, da 28 a 38 gradi, ricche di minerali benefici per la pelle e l'apparato muscolo-scheletrico. Il percorso prevede anche saune di vapore termale, idromassaggi naturali e cascate per il massaggio cervicale. Un esperto ti guiderà tra le varie tappe del percorso, consigliandoti i tempi ottimali di permanenza in ciascuna area. L'esperienza include un trattamento di fangoterapia e si conclude con una tisana rilassante nella sala relax panoramica, con vista sul parco termale.", "Rilassati nelle acque termali naturali di una grotta millenaria", "3 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 3, 22, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 22, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Terme Naturali", "Saturnia, Toscana", 95m, "Percorso benessere in grotta termale", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 },
                    { new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), new Guid("6f3a957c-df09-437c-bc37-f069173eabe2"), "93a1a227-6394-49fe-8ab9-b26b14cfd0e9.jpg", "Vivi la Toscana come nei film, guidando una Vespa d'epoca attraverso le splendide colline del Chianti. Questo tour ti offre un'esperienza autentica e indimenticabile, combinando il piacere della guida di questo iconico scooter italiano con la scoperta dei paesaggi più belli della campagna toscana. Dopo un briefing iniziale sulla guida della Vespa, partirai in un piccolo gruppo lungo strade secondarie poco trafficate, attraversando vigneti, oliveti e cipressi che caratterizzano il paesaggio. Il percorso include soste in borghi medievali caratteristici, come Greve in Chianti e Castellina, dove potrai passeggiare tra botteghe artigiane e degustare prodotti locali. A metà giornata, è previsto un pranzo in un'azienda agricola a conduzione familiare, dove gusterai piatti tradizionali toscani accompagnati dai famosi vini del Chianti. Il tour include anche una visita a una cantina storica con degustazione di vini.", "Esplora le colline del Chianti in sella a un'icona italiana", "8 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 6, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Toscana in Moto", "Firenze, Toscana", 180m, "Tour in vespa nella campagna toscana", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 },
                    { new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), new Guid("6accf29d-8d1c-4edd-b48a-c70251516b99"), "68233a60-a0ce-4a7c-9049-5fd6dd3981fd.jpg", "Scopri la bellezza unica delle Cinque Terre dal punto di vista più suggestivo: il mare. Questo tour in barca ti porta lungo la costa della Riviera Ligure per ammirare i cinque iconici villaggi di Monterosso, Vernazza, Corniglia, Manarola e Riomaggiore, con le loro case colorate che sembrano aggrappate alle scogliere. Durante la navigazione, la guida ti racconterà la storia e le curiosità di questo territorio dichiarato Patrimonio dell'Umanità UNESCO. L'escursione include soste per fare il bagno nelle calette più belle, difficilmente raggiungibili da terra, e un pranzo a bordo con prodotti tipici locali e vino bianco delle Cinque Terre. È prevista anche una sosta in uno o due villaggi per una breve esplorazione a piedi.", "Ammira i colorati villaggi delle Cinque Terre dal mare", "7 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 6, 1, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 1, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Navigazione Ligure", "Cinque Terre, Liguria", 120m, "Tour in barca delle Cinque Terre", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 },
                    { new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), "68c11a26-bcf2-45d5-b1a4-0d230963fa9e.jpg", "Diventa pizzaiolo per un giorno e apprendi l'arte della vera pizza napoletana, patrimonio UNESCO, sotto la guida di un maestro pizzaiolo campione mondiale. Questa esperienza hands-on ti permetterà di conoscere tutti i segreti di questo piatto iconico, dalla preparazione dell'impasto alla stesura, fino alla cottura nel forno a legna. La lezione inizia con una breve storia della pizza e una spiegazione degli ingredienti tradizionali, tutti di altissima qualità e a km zero. Imparerai a preparare l'impasto con la giusta idratazione, i tempi di lievitazione e la tecnica per stenderlo a mano, creando il caratteristico bordo alto della pizza napoletana. Ogni partecipante preparerà diverse pizze con vari condimenti, dalla classica Margherita alla Marinara, fino a creazioni più elaborate con ingredienti stagionali. Naturalmente, potrai gustare le pizze che preparerai, accompagnate da birra artigianale o vino locale. Al termine, riceverai un attestato di partecipazione e la ricetta originale per continuare a preparare la pizza a casa.", "Impara i segreti della vera pizza napoletana da un maestro", "3 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 7, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Cucina Napoletana", "Napoli, Campania", 89m, "Lezione di pizza napoletana con pizzaiolo campione", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsInEvidence", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[,]
                {
                    { new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), new Guid("6f3a957c-df09-437c-bc37-f069173eabe2"), "372a18e3-7932-4ef5-8471-99ce5f3e098a.jpg", "Vivi l'emozione di guidare una Ferrari F488 GTB sul mitico circuito di Formula 1 di Monza. Dopo un briefing teorico con un pilota professionista, avrai l'opportunità di metterti al volante di questa supercar italiana e percorrere diversi giri sul circuito. Sentirai l'adrenalina scorrere mentre acceleri sui rettilinei e affronti le curve leggendarie come la Parabolica. Un'esperienza che combina lusso, velocità e il fascino intramontabile del marchio Ferrari. Il pacchetto include anche un video ricordo della tua esperienza in pista.", "Guida una Ferrari sul leggendario circuito di Monza", "2 ore", "Tutto il necessario per la guida: casco, tuta e assicurazione. Video ricordo dell'esperienza.", true, true, new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Motor Experience", "Monza, Lombardia", 399m, "Ferrari Driving Experience a Monza", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 24 },
                    { new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), "350540d5-80b9-49fa-8fb2-8a58c80d149c.jpg", "Questa esperienza culinaria si svolge in un autentico castello toscano circondato da vigneti e uliveti. Sotto la guida di uno chef locale, imparerai a preparare un menu completo di piatti tradizionali toscani utilizzando ingredienti freschi provenienti direttamente dall'orto del castello e da produttori locali. Il corso inizia con una passeggiata nei giardini per raccogliere erbe aromatiche, seguita dalla preparazione di pasta fatta in casa, un secondo a base di carne e un dolce tipico. Al termine della lezione, gusterai i piatti preparati accompagnati dai vini prodotti nella tenuta. Un'esperienza che coinvolge tutti i sensi e ti permette di portare a casa ricette e tecniche autentiche.", "Impara a cucinare i piatti tradizionali toscani con la guida di uno chef locale", "5 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 3, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Sapori d'Italia", "Chianti, Toscana", 150m, "Cucina toscana nella tenuta di un castello", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), new Guid("1652310e-b8f3-43e7-bd9d-287f73f939b5"), "5b23a8b9-9686-4107-b2ff-2a468167a32f.jpg", "Un'avventura nella natura selvaggia della Maremma toscana, alla ricerca degli animali che popolano questo territorio unico. Guidato da un esperto naturalista e fotografo professionista, esplorerai la Riserva Naturale della Maremma a bordo di un fuoristrada 4x4, addentrandoti in aree normalmente non accessibili al pubblico. Il safari inizia all'alba, quando gli animali sono più attivi, e ti porterà attraverso diversi habitat, dalle zone umide alle foreste, dalle praterie alle dune costiere. Con un po' di fortuna e pazienza, potrai avvistare e fotografare cervi maremmani, cinghiali, daini, volpi, istrici, e numerose specie di uccelli, tra cui fenicotteri e rapaci. La guida ti fornirà consigli sulla fotografia naturalistica e ti spiegherà curiosità sulla flora e fauna locali. L'esperienza include una colazione tipica toscana consumata all'aperto e un breve workshop fotografico per migliorare le tue capacità di catturare immagini di animali selvatici.", "Avvista e fotografa cervi, cinghiali e altri animali selvatici", "5 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 4, 22, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 22, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Natura Toscana", "Grosseto, Toscana", 110m, "Safari fotografico in Maremma", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsPopular", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), "4949849a-cc7a-4481-9c77-929fdbb71310.jpg", "Un'esperienza sensoriale completa nel cuore della campagna toscana, in una cantina storica scavata nel tufo. Questa degustazione guidata ti permetterà di conoscere i segreti della vinificazione, dalla coltivazione dell'uva all'invecchiamento in botte. Accompagnato da un sommelier esperto, percorrerai le gallerie sotterranee dove riposano le botti centenarie, mantenute alla temperatura perfetta dal microclima naturale. La degustazione include sei vini pregiati della tenuta, tra cui Chianti Classico, Super Tuscan e Vin Santo, accompagnati da pane toscano, olio extravergine d'oliva di produzione propria, formaggi locali e salumi artigianali. Durante l'esperienza, imparerai le tecniche di degustazione professionale e come abbinare correttamente il vino al cibo.", "Scopri i segreti del vino in una cantina medievale", "2 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 7, 20, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 20, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Cantine Toscane", "Montepulciano, Toscana", 75m, "Degustazione di vini in cantina sotterranea", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), "ec367062-0089-4605-9646-5c52baaff96e.jpg", "Immergiti nell'antica tradizione della ricerca del tartufo, accompagnando un tartufaio esperto e i suoi cani addestrati in un'escursione nei boschi dell'Umbria. Questa esperienza autentica ti permetterà di scoprire i segreti di una pratica riconosciuta dall'UNESCO come patrimonio immateriale dell'umanità. L'avventura inizia all'alba, quando il tartufaio ti accoglierà nella sua casa per una colazione con prodotti locali e ti presenterà i suoi fedeli compagni a quattro zampe, spiegandoti come vengono addestrati fin da cuccioli a riconoscere il profumo del tartufo. Ti forniranno gli stivali e un vanghetto, per poi avviarti nei boschi di querce e noccioli dove crescono i preziosi tartufi neri o bianchi (a seconda della stagione). Osserverai da vicino come i cani, con il loro fiuto eccezionale, individuano il punto esatto dove scavare e come il tartufaio estrae con cura il fungo dal terreno, preservando il micelio per le future produzioni. Al termine della ricerca, verrai accompagnato in un ristorante locale dove lo chef preparerà un pranzo completo utilizzando i tartufi appena trovati, accompagnato dai migliori vini umbri. L'esperienza include un barattolo di pasta al tartufo da portare a casa come ricordo.", "Cerca il prezioso fungo ipogeo nelle foreste umbre", "6 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 10, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Tesori dell'Umbria", "Norcia, Umbria", 125m, "Caccia al tartufo con cani addestrati", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsPopular", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Sale", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), new Guid("6accf29d-8d1c-4edd-b48a-c70251516b99"), "a8b9cf9c-4f8e-4e18-8413-bf5de4cb4b3c.jpg", "Preparati a vivere un'avventura emozionante nelle acque cristalline del fiume Nera, situato nel cuore dell'Umbria. Questa esperienza di rafting ti permetterà di affrontare rapide di diversa difficoltà sotto la guida di istruttori professionisti. Prima di iniziare, riceverai un briefing completo sulla sicurezza e sulle tecniche di pagaiata. Il percorso si snoda attraverso paesaggi selvaggi e incontaminati, con gole spettacolari e cascate. Durante le pause potrai anche fare il bagno nelle piscine naturali del fiume. L'attività è adatta sia a principianti che a esperti.", "Un'avventura adrenalinica tra le rapide di uno dei fiumi più belli dell'Umbria", "4 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 7, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Avventure Italiane", "Scheggino, Umbria", 85m, 5, "Rafting nelle rapide del fiume Nera", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[,]
                {
                    { new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), new Guid("1652310e-b8f3-43e7-bd9d-287f73f939b5"), "5523143a-02f3-44ef-abcd-5dca1aa1a42c.jpg", "Sfida te stesso con un'esperienza di arrampicata sulle Dolomiti, le spettacolari montagne dichiarate Patrimonio UNESCO. Questa avventura verticale, guidata da istruttori qualificati con anni di esperienza, ti porterà a scalare alcune delle vie più panoramiche delle Pale di San Martino. L'esperienza è personalizzata in base al tuo livello di abilità, con percorsi adatti sia ai principianti assoluti che ai climber più esperti. La giornata inizia con un briefing tecnico e la distribuzione dell'attrezzatura (imbracatura, casco, scarpette da arrampicata), seguito da una breve camminata fino alla parete. Qui l'istruttore ti insegnerà le tecniche di base o avanzate di arrampicata e sicurezza, prima di affrontare le vie selezionate. Durante l'ascensione, potrai godere di viste mozzafiato sulle valli sottostanti e sui panorami montani circostanti. L'esperienza include foto e video della tua arrampicata, che ti verranno inviate dopo l'attività.", "Scala le pareti verticali delle montagne più belle del mondo", "6 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 6, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Alpine Guides", "San Martino di Castrozza, Trentino", 130m, "Arrampicata sulle Dolomiti", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 },
                    { new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), new Guid("48733fb8-deae-41b2-b0c6-4fab3c45cf93"), "9b5946b1-66a5-4e0a-89fb-0c0f026c5431.jpg", "Scopri i segreti di Pompei, la città romana perfettamente conservata sotto le ceneri dell'eruzione del Vesuvio del 79 d.C., con una visita guidata esclusiva da un archeologo che ha partecipato agli scavi. Questo tour privato ti permetterà di accedere ad aree normalmente chiuse al pubblico e di comprendere in profondità la vita quotidiana dei romani, grazie alle spiegazioni dettagliate di un esperto del settore. La visita inizia dalle terme pubbliche e prosegue attraverso il foro, i teatri, le case patrizie riccamente decorate con affreschi e mosaici, fino ai lupanari e alle botteghe, ricostruendo la vita di diverse classi sociali dell'epoca. L'archeologo ti mostrerà i calchi in gesso delle vittime, spiegando le tecniche di scavo e conservazione, e ti illustrerà le ultime scoperte avvenute nel sito. Durante il tour, potrai fare tutte le domande che desideri all'esperto, che condividerà anche aneddoti e curiosità legati al suo lavoro negli scavi. L'esperienza include l'ingresso prioritario senza attesa in coda e un libro fotografico su Pompei come ricordo.", "Esplora la città romana sepolta dall'eruzione del Vesuvio", "3 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 3, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Archeologia Viva", "Pompei, Campania", 120m, "Visita agli scavi di Pompei con archeologo", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsPopular", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Sale", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), new Guid("da780fcf-074e-4e0c-b0b8-1bd8e0c0fa6f"), "b33390f8-430a-456c-b821-83a8b9406043.jpg", "Un'esperienza indimenticabile che ti permetterà di ammirare il magnifico paesaggio toscano da una prospettiva unica. Il volo in mongolfiera al tramonto inizia con un brindisi di benvenuto mentre l'equipaggio prepara il pallone. Durante l'ascesa potrai godere della vista spettacolare delle colline dorate dal sole al tramonto, dei vigneti e degli uliveti che caratterizzano questa splendida regione. Il volo dura circa un'ora e si conclude con un brindisi con prosecco locale e uno spuntino con prodotti tipici.", "Goditi un tramonto mozzafiato sorvolando le colline toscane", "3 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 6, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Avventure Italiane", "Siena, Toscana", 249.99m, 5, "Volo in mongolfiera al tramonto", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 8 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), new Guid("6accf29d-8d1c-4edd-b48a-c70251516b99"), "10e151bb-bcf2-4b5e-9de4-b5b27fbd1036.jpg", "Un'avventura subacquea unica per esplorare i relitti storici della Seconda Guerra Mondiale adagiati sui fondali del Mar Tirreno. Questa esperienza, guidata da istruttori specializzati in immersioni su relitti, ti porterà alla scoperta di navi militari, mercantili e aerei da guerra perfettamente conservati nelle acque cristalline al largo dell'Isola d'Elba. L'escursione inizia al mattino con un briefing dettagliato sulla storia dei relitti che visiterai, sulle loro caratteristiche e sulle tecniche di immersione in sicurezza. A bordo di un'imbarcazione attrezzata per le immersioni, raggiungerai il punto di interesse, dove potrai esplorare relitti come il mercantile KT, affondato nel 1943, o i resti di un bombardiere americano B-24, immersi in un ecosistema marino rigoglioso che ha colonizzato le strutture metalliche. Durante l'immersione, la guida ti mostrerà i dettagli più interessanti di questi reperti storici, come la sala macchine, il ponte di comando o i portelli dei siluri. L'esperienza prevede due immersioni in siti diversi, con una pausa pranzo a bordo dell'imbarcazione. Al termine, riceverai un attestato di partecipazione e un reportage fotografico dell'immersione. Requisito necessario: essere in possesso di un brevetto subacqueo Advanced Open Water o equivalente.", "Esplora navi e aerei militari sommersi nel Mar Mediterraneo", "8 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 5, 25, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 25, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Dive History", "Isola d'Elba, Toscana", 180m, "Immersione tra i relitti della Seconda Guerra Mondiale", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsInEvidence", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), new Guid("bda4ee71-af9c-46c6-b1bf-95f178773a2f"), "f34f2a25-8e55-4826-8ce4-aca2a2a76c3a.jpg", "Il Sentiero degli Dei è uno dei percorsi di trekking più affascinanti d'Italia, che collega Agerola a Positano offrendo viste mozzafiato sulla Costiera Amalfitana. Questa escursione guidata ti porterà lungo antiche mulattiere e sentieri di montagna, attraverso terrazzamenti coltivati a limoni e ulivi, macchia mediterranea e boschi. Durante il cammino, potrai ammirare il blu intenso del mar Tirreno, le isole Li Galli e Capri, e i caratteristici villaggi aggrappati alle scogliere. La guida ti racconterà la storia e le leggende locali, e ti indicherà i luoghi migliori per scattare fotografie indimenticabili.", "Esplora il famoso sentiero con viste panoramiche sulla Costiera Amalfitana", "6 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 4, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 5, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Italia Escursioni", "Costiera Amalfitana, Campania", 65m, "Trekking sul sentiero degli Dei", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 6 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "IsPopular", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[] { new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), new Guid("48733fb8-deae-41b2-b0c6-4fab3c45cf93"), "280b9c0f-257e-4f05-b2db-84e704fda33d.jpg", "Vivi l'esperienza di fotografare Venezia in un momento magico, quando la città è avvolta nella luce dorata dell'alba e le strade sono ancora tranquille, senza turisti. Questo tour fotografico guidato da un fotografo professionista ti porterà nei luoghi più iconici e nei angoli nascosti della Serenissima, offrendoti l'opportunità di catturare immagini uniche. Il tour inizia in Piazza San Marco, quando è ancora deserta, per poi esplorare i canali minori, i ponti caratteristici e i campielli pittoreschi. Durante il percorso, riceverai consigli tecnici personalizzati e suggerimenti creativi per migliorare le tue fotografie. Il tour è adatto sia ai principianti che ai fotografi esperti, con qualsiasi tipo di macchina fotografica, anche smartphone.", "Cattura l amagia di Venezia nelle prime luci del giorno", "3 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, true, new DateTime(2023, 5, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Venezia Autentica", "Venezia, Veneto", 95m, "Tour fotografico di Venezia all'alba", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 8 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "CategoryId", "CoverImage", "Description", "DescriptionShort", "Duration", "IncludedDescription", "IsFreeCancellable", "LastEditDate", "LoadingDate", "MaxParticipants", "Organiser", "Place", "Price", "Title", "UserCreatorId", "UserLastModifyId", "ValidityInMonths" },
                values: new object[,]
                {
                    { new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), new Guid("48733fb8-deae-41b2-b0c6-4fab3c45cf93"), "df4ae3d4-162f-4c80-85c9-c7fad7ff62b1.jpg", "Vivi l'emozione di assistere a una rappresentazione di teatro classico greco nel maestoso Teatro Antico di Taormina, con vista sull'Etna e sul Mar Ionio. Questa esperienza culturale unica ti riporterà indietro nel tempo di 2300 anni, permettendoti di apprezzare le opere di Eschilo, Sofocle o Euripide nello stesso tipo di ambientazione per cui furono scritte. Lo spettacolo inizia al tramonto, quando le luci naturali creano un'atmosfera magica e suggestiva sul palcoscenico. Prima della rappresentazione, parteciperai a un tour guidato esclusivo del teatro, normalmente non accessibile al pubblico, in cui l'archeologo ti illustrerà la storia e i segreti di questo monumento, spiegando l'acustica perfetta e le tecniche teatrali dell'antica Grecia. Il pacchetto include posti riservati nelle prime file, un programma dettagliato con traduzione in italiano del testo originale greco, e un calice di vino dell'Etna da degustare durante l'intervallo, mentre il cielo si colora di rosso e il vulcano si staglia all'orizzonte.", "Assisti a una tragedia greca in un teatro di 2300 anni fa", "3 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 5, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Sicilia Culturale", "Taormina, Sicilia", 90m, "Teatro greco antico al tramonto", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 },
                    { new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), new Guid("1652310e-b8f3-43e7-bd9d-287f73f939b5"), "cf8e8bf0-59b5-44b2-bb5c-478b18b7f767.jpg", "Un'esperienza mozzafiato sul vulcano attivo più grande d'Europa. Questa escursione notturna ti porterà a scoprire l'affascinante paesaggio lunare dell'Etna quando il buio avvolge il vulcano e le stelle brillano intense nel cielo siciliano. Accompagnato da guide vulcanologiche esperte, inizierai il percorso al tramonto per raggiungere i punti panoramici dove godere della vista sulla costa e sui paesi etnei che si illuminano con il calare della notte. Durante il cammino su sentieri di lava solidificata, potrai osservare le colate recenti e i crateri secondari, ascoltando il racconto delle eruzioni storiche e dei miti legati a questo vulcano. Al calare completo della notte, con l'aiuto di lampade frontali, raggiungerai un punto di osservazione privilegiato da cui, con un po' di fortuna, potrai vedere il bagliore rossastro dell'attività vulcanica. L'escursione include una sosta per una cena al sacco in un rifugio di montagna, con degustazione di vini dell'Etna.", "Ammira il vulcano attivo sotto il cielo stellato", "6 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 4, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Sicilia Avventure", "Catania, Sicilia", 120m, "Escursione notturna sull'Etna", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 24 },
                    { new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), new Guid("7f13b386-b8af-4ed1-b42b-845e17f657c3"), "b3a2b770-e6b4-44e0-aa25-15d7447d1200.jpg", "Lasciati trasportare dalla magia di Venezia con un romantico tour in gondola al tramonto, accompagnato da una tradizionale serenata italiana eseguita dal vivo. Questa esperienza esclusiva ti permetterà di navigare i pittoreschi canali della Serenissima, passando sotto ponti storici e ammirando palazzi aristocratici, mentre il sole cala tingendo di rosa e oro i marmi e l'acqua della laguna. La tua gondola privata sarà guidata da un gondoliere esperto in costume tradizionale, che ti racconterà aneddoti e curiosità sulla città e sulla sua professione secolare. A bordo, un musicista professionista (fisarmonica o chitarra) e un cantante lirico eseguiranno per te classiche canzoni italiane e arie d'opera famose, creando un'atmosfera indimenticabile. Il tour parte dal Canal Grande e si addentra nei canali minori, lontano dalla folla, per regalarti scorci autentici e tranquilli della città. L'esperienza include una bottiglia di prosecco da sorseggiare durante il tragitto e si conclude con una rosa per la tua accompagnatrice.", "Naviga i canali di Venezia al tramonto con musica dal vivo", "1 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 7, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), 2, "Venezia Autentica", "Venezia, Veneto", 250m, "Tour in gondola al tramonto con serenata", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c", 24 },
                    { new Guid("ff3ed239-e178-4632-8385-042286991c66"), new Guid("a4049ef8-1e86-48bf-b514-3930469ddcbd"), "5f37b647-e33d-440e-88ed-2e0d956f377a.jpg", "Esplora le colline del Montefeltro, al confine tra Marche, Toscana ed Emilia-Romagna, a bordo di una moderna e-bike che ti permetterà di percorrere distanze importanti senza eccessiva fatica. Questo tour guidato ti porterà alla scoperta di antichi borghi medievali, castelli rinascimentali e abbazie secolari, immersi in un paesaggio che ha ispirato i fondali di molti dipinti di Piero della Francesca. Partendo da Urbino, patrimonio UNESCO e culla del Rinascimento italiano, pedalerai lungo strade secondarie poco trafficate e sentieri panoramici, visitando perle nascoste come San Leo, con la sua imponente fortezza, Pennabilli, con i suoi misteriosi giardini, e Sant'Agata Feltria, famosa per il tartufo. Durante il percorso, sono previste soste per visite culturali e per degustare prodotti tipici locali, come il formaggio di fossa, il prosciutto di Carpegna e i vini del territorio. La guida ti racconterà la storia e le leggende di questi luoghi, svelando curiosità e aneddoti poco conosciuti. L'esperienza è adatta a tutti, grazie all'assistenza elettrica delle biciclette che rende accessibili anche i tratti in salita.", "Pedalata assistita tra castelli, abbazie e panorami mozzafiato", "7 ore", "L'esperienza include tutto il necessario per goderti l'avventura in totale sicurezza e comfort.", true, new DateTime(2023, 8, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 10, 11, 0, 56, 0, DateTimeKind.Unspecified), 1, "Marche Experience", "Urbino, Marche", 70m, "Escursione in e-bike nei borghi del Montefeltro", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", "3a8073b2-b954-428a-a4b9-6e4b3f5db051", 12 }
                });

            migrationBuilder.InsertData(
                table: "FidelityCards",
                columns: new[] { "FidelityCardId", "AvailablePoints", "CardNumber", "Points", "UserId" },
                values: new object[] { new Guid("12d1edf2-df86-41d9-8594-0b1859e31932"), 445, "454432678900", 445, "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2" });

            migrationBuilder.InsertData(
                table: "FidelityCards",
                columns: new[] { "FidelityCardId", "CardNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("326ddfe3-754b-4f24-8cd6-1011bc3cc37e"), "453728123423", "d9ee1702-09f8-4ec2-ac09-7f41c05fcd4c" },
                    { new Guid("3f05415d-e413-4430-bdcd-e668d6f7aa83"), "873524120039", "e5675086-e91e-442a-9c22-27d41bee49a4" },
                    { new Guid("772e32cc-cdea-4413-8785-09312f52f33d"), "341020401032", "3a8073b2-b954-428a-a4b9-6e4b3f5db051" }
                });

            migrationBuilder.InsertData(
                table: "FidelityCards",
                columns: new[] { "FidelityCardId", "AvailablePoints", "CardNumber", "Points", "UserId" },
                values: new object[] { new Guid("ac983a29-21fe-4d7b-822f-2de328dee367"), 1331, "123245783911", 1331, "698c347e-bb57-4cb4-b672-9940647f250d" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreatedAt", "OrderNumber", "ShippingPrice", "State", "SubTotalPrice", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { new Guid("089b2a7e-4287-4e1c-8928-693a736db304"), new DateTime(2024, 2, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), 123450, 4.99m, "Completato", 145m, 149.99m, "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2" },
                    { new Guid("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"), new DateTime(2025, 2, 23, 11, 0, 56, 0, DateTimeKind.Unspecified), 123453, 4.99m, "Completato", 360m, 364.99m, "698c347e-bb57-4cb4-b672-9940647f250d" },
                    { new Guid("cf854aee-04c4-43ff-bb30-445daa75478a"), new DateTime(2024, 7, 25, 11, 0, 56, 0, DateTimeKind.Unspecified), 123451, 4.99m, "Completato", 300m, 304.99m, "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2" },
                    { new Guid("d1f55060-cb7a-4c66-b674-adda6099dde5"), new DateTime(2025, 4, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), 123454, 4.99m, "In attesa", 479.75m, 484.74m, "698c347e-bb57-4cb4-b672-9940647f250d" },
                    { new Guid("dc2a8bdd-f6cc-4637-9104-639f0e020777"), new DateTime(2025, 1, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), 123452, 4.99m, "Completato", 492.49m, 497.48m, "698c347e-bb57-4cb4-b672-9940647f250d" }
                });

            migrationBuilder.InsertData(
                table: "CarryWiths",
                columns: new[] { "CarryWithId", "ExperienceId", "Name" },
                values: new object[,]
                {
                    { new Guid("08a1412a-0b48-4917-9f3a-ee1f900207f0"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), "Scarpe adatte all'attività" },
                    { new Guid("090db950-0043-4e93-98ed-6f65927b08d6"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Abbigliamento comodo" },
                    { new Guid("112e26dc-ef17-4168-bcdf-cc6e268f9d95"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), "Scarpe adatte all'attività" },
                    { new Guid("18994487-ba73-42ed-bd80-48fd3dfa3df0"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), "Macchina fotografica (opzionale)" },
                    { new Guid("1b761e61-ff60-4730-acc6-ce4fb3613ab4"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), "Abbigliamento comodo" },
                    { new Guid("1db350e1-5cb8-4d9f-ac82-1ba56f938047"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Documento d'identità" },
                    { new Guid("243df698-9bf6-4ec9-bc7f-9cbe3e02398d"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Scarpe adatte all'attività" },
                    { new Guid("25860663-e017-4e55-91f0-52e45470f4d3"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Acqua" },
                    { new Guid("2a29efe4-a786-472c-8696-1b41c183d5cc"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), "Acqua" },
                    { new Guid("2a5766d0-8207-4c27-8569-b4d6fe09ea56"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), "Abbigliamento comodo" },
                    { new Guid("2d6da007-4afa-4830-86d4-cdf5d7244de6"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Scarpe adatte all'attività" },
                    { new Guid("2f553589-bac0-4a7f-a245-7a0d528c6573"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), "Macchina fotografica (opzionale)" },
                    { new Guid("3339d17f-4fd0-4942-b1de-fc2ba54c15fe"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Macchina fotografica (opzionale)" },
                    { new Guid("339a7c4f-71db-4c13-9c9d-b678b2ccaf76"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), "Abbigliamento comodo" },
                    { new Guid("36ff9655-7ad0-4002-8e18-33a9b95f1dc5"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Scarpe adatte all'attività" },
                    { new Guid("37f66586-377d-48b0-8775-0f3ab4743fad"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Abbigliamento comodo" },
                    { new Guid("39fc5e4e-42c8-45dc-8412-7718ed2c3762"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), "Scarpe adatte all'attività" },
                    { new Guid("3a0c8fb7-1794-4233-8d03-f65419d22648"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), "Scarpe adatte all'attività" },
                    { new Guid("3a73b99c-bc56-426d-aa3b-2668d270a81c"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Abbigliamento comodo" },
                    { new Guid("3b853054-0002-478d-a8d9-387e92a6e44c"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), "Documento d'identità" },
                    { new Guid("3f07f4ae-d6a8-4863-ab37-2fad83952f9a"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), "Documento d'identità" },
                    { new Guid("40070abb-16f7-426e-8768-2f77bc7bc753"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Abbigliamento comodo" },
                    { new Guid("41d7915f-74e1-41b8-b20b-49282d149a0a"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), "Documento d'identità" },
                    { new Guid("44245cfb-c6b8-46d3-a4da-374a4595cbda"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Abbigliamento comodo" },
                    { new Guid("44ca62ed-9961-4d8e-b236-73b3a775227d"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Macchina fotografica (opzionale)" },
                    { new Guid("457ccc48-cca1-4c79-825b-6e6accdef6ba"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Scarpe adatte all'attività" },
                    { new Guid("4607945b-e5c3-41d5-9984-0e6e9ebbb740"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Abbigliamento comodo" },
                    { new Guid("4671f2cb-60e1-41fa-81da-2b4cbfa203a7"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), "Acqua" },
                    { new Guid("4dc78b29-e9b1-4fd0-8965-a59dae5ac784"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Acqua" },
                    { new Guid("4ef3c543-9181-4fb9-8bb2-7433965af376"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Acqua" },
                    { new Guid("5440ef3c-3a70-441f-a437-8d83c846ab50"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Scarpe adatte all'attività" },
                    { new Guid("5687e531-773b-4855-b3c0-d8124c3f325c"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Macchina fotografica (opzionale)" },
                    { new Guid("5ce0813b-3969-48cd-ba31-f649881c2f85"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Scarpe adatte all'attività" },
                    { new Guid("65b2065f-34ab-4a9d-a83a-aef299fde5d5"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), "Acqua" },
                    { new Guid("6c3395e2-bbeb-420b-bbfd-c22f822ed11e"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), "Macchina fotografica (opzionale)" },
                    { new Guid("710cd42f-ed8d-4fc0-8174-addb9807dc29"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), "Acqua" },
                    { new Guid("751a3067-da26-4bbf-8dfd-d688de8078ac"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), "Macchina fotografica (opzionale)" },
                    { new Guid("7b1a1b63-3584-4849-ba0d-1ccb54fd0fe3"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Scarpe adatte all'attività" },
                    { new Guid("7b4712ec-b86b-42b4-bde9-24b089ac4b77"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Acqua" },
                    { new Guid("7ba9453c-a316-4fcc-a45b-99b8d4afab90"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Documento d'identità" },
                    { new Guid("7cf387a9-01fc-46c1-9fb3-19c09ed61884"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), "Documento d'identità" },
                    { new Guid("7dfb84a9-3798-4fce-ad7c-bfe60abb74b8"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), "Scarpe adatte all'attività" },
                    { new Guid("7e6b3dc0-a425-42e0-b8b7-2e9a27ddc342"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), "Scarpe adatte all'attività" },
                    { new Guid("7f618cc7-2a38-4219-8ca8-84ac0541576e"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Acqua" },
                    { new Guid("7fc7077e-779c-4eec-ae5c-f642d9685b15"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), "Acqua" },
                    { new Guid("81da796b-adec-4023-b39d-017d9adfaca9"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), "Abbigliamento comodo" },
                    { new Guid("82b97673-c6cd-4dd2-8e10-27e93e19fec1"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Acqua" },
                    { new Guid("87f70607-bfab-46f0-988b-20a7e42c55ac"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), "Macchina fotografica (opzionale)" },
                    { new Guid("90df94c4-8281-46f7-97de-357044e18877"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), "Scarpe adatte all'attività" },
                    { new Guid("967d6d6f-f4f5-45b6-971a-2bc96cfc970a"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), "Abbigliamento comodo" },
                    { new Guid("9bfe1a7a-4a3a-4a17-801e-a071c5a70361"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), "Acqua" },
                    { new Guid("9e61d200-f670-4b5c-ba28-64a7ed1b63e4"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), "Documento d'identità" },
                    { new Guid("9ea9e04b-9fb7-41a2-a9b1-76e23f0ef97c"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), "Abbigliamento comodo" },
                    { new Guid("9f16a4a2-1fbf-4fad-b5a5-2d3318ebb5dc"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Abbigliamento comodo" },
                    { new Guid("a1761613-4056-4ca9-84ed-3de05cb7827c"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), "Scarpe adatte all'attività" },
                    { new Guid("a39149a6-00f8-44ec-a9a6-133560e8e73e"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Documento d'identità" },
                    { new Guid("a7e1b81a-d252-435a-9e50-48b5549e2aff"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Acqua" },
                    { new Guid("a85cef09-4f5f-4787-af53-977737955c1f"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Documento d'identità" },
                    { new Guid("aaaa9a81-b420-45a9-9164-4640382cca71"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Abbigliamento comodo" },
                    { new Guid("b242290d-8ce3-49d3-bff7-01b7f6c40b2b"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), "Macchina fotografica (opzionale)" },
                    { new Guid("b47847bd-c782-49fb-a7f8-b9449a2680b0"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Acqua" },
                    { new Guid("b6457b26-bef6-47b4-9637-aa49d76c43fd"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Documento d'identità" },
                    { new Guid("b7125708-7e08-4c1b-8bc7-4cb68bf36a1c"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Documento d'identità" },
                    { new Guid("bcd5eacd-61b5-4bb7-986a-ec7ab5a13f69"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Macchina fotografica (opzionale)" },
                    { new Guid("bea4bfa2-3f89-42fd-9074-9aea2c4cac34"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), "Abbigliamento comodo" },
                    { new Guid("c7dcca56-a4f8-42a6-8091-718944a2163c"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), "Abbigliamento comodo" },
                    { new Guid("ca9314a7-aaa7-4eb9-ba96-5fb79448f763"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), "Macchina fotografica (opzionale)" },
                    { new Guid("cc6c0540-3ad0-46a9-b75d-16d505f6e2e4"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), "Abbigliamento comodo" },
                    { new Guid("cebce589-6d9e-4c93-9ffb-769879360a58"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Abbigliamento comodo" },
                    { new Guid("cf9ffc4f-0db2-4957-a51d-2b919d755705"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Documento d'identità" },
                    { new Guid("d2d39432-5c69-4c6c-a21b-498c55792708"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), "Macchina fotografica (opzionale)" },
                    { new Guid("d4ad2834-0a9e-4a25-ab65-67fc7e9a2856"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Acqua" },
                    { new Guid("d776ef24-0fa2-4bd3-a693-8468da64aac9"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), "Documento d'identità" },
                    { new Guid("dd784099-5b3e-4231-bc9d-4c45cdc1e354"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Scarpe adatte all'attività" },
                    { new Guid("dd9587fd-6e3f-4a32-88e3-39291155c3c8"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), "Acqua" },
                    { new Guid("ddadd752-df1c-43f7-9466-dcb72fbcf262"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Macchina fotografica (opzionale)" },
                    { new Guid("dfc0f099-4e30-4cef-a667-7aed81f44e24"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), "Documento d'identità" },
                    { new Guid("e179f003-23b8-4714-9d3c-d911f9b19cf0"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), "Abbigliamento comodo" },
                    { new Guid("e2669eb7-0538-4ce0-9098-6916b04e7c1e"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "Macchina fotografica (opzionale)" },
                    { new Guid("e2aa3ea4-4387-4f80-90df-c6527a217869"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), "Documento d'identità" },
                    { new Guid("e4381c78-a607-4b61-8eaa-fa65559a9e2c"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), "Acqua" },
                    { new Guid("e4cc19b6-e764-4bee-a931-ea4118a3684e"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), "Scarpe adatte all'attività" },
                    { new Guid("e6c86760-cf4c-41a5-8450-e085d3c407ec"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "Documento d'identità" },
                    { new Guid("e87d8136-db50-4e14-b04e-3b0890d47b31"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), "Scarpe adatte all'attività" },
                    { new Guid("e90eb389-a733-4917-8c57-24bfbd072dc4"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Acqua" },
                    { new Guid("ea349b5b-5dc5-4d56-9d9c-36f776dbe2c3"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), "Documento d'identità" },
                    { new Guid("eba64c18-8396-4bcd-b169-e262b1dfa061"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "Scarpe adatte all'attività" },
                    { new Guid("ed314f33-6a11-4c0e-b570-c7dd3b43017d"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "Documento d'identità" },
                    { new Guid("eda27243-f2b3-449c-b0bd-21aeca92729a"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), "Macchina fotografica (opzionale)" },
                    { new Guid("ee348b02-77df-4d20-ab9f-f456c0dbb1f0"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), "Acqua" },
                    { new Guid("f282cc6a-1c66-4a00-8e20-0e722a954254"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), "Macchina fotografica (opzionale)" },
                    { new Guid("f4237517-e5cf-4b3a-97c5-9fc40b26351a"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "Scarpe adatte all'attività" },
                    { new Guid("f4d999e5-74db-44c3-9f55-8296cfd4f6a4"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Documento d'identità" },
                    { new Guid("f4f28288-636e-4660-ac81-483be912d4f1"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), "Documento d'identità" },
                    { new Guid("f5673096-288f-4a8d-a706-800cb9739930"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "Macchina fotografica (opzionale)" },
                    { new Guid("f7b79ffb-a746-4794-9dc7-c6981f23b76d"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "Macchina fotografica (opzionale)" },
                    { new Guid("f7b8e6a9-267a-4cb1-8823-3d691d15a3e4"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "Macchina fotografica (opzionale)" },
                    { new Guid("f82cbfbf-d01a-40ca-b175-20366f94fb4a"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "Abbigliamento comodo" },
                    { new Guid("fa3469c9-f1a1-4ce0-be74-58aea3289418"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "Macchina fotografica (opzionale)" },
                    { new Guid("fa647bce-8939-4618-8d99-5432d12c5bf1"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), "Acqua" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("00889ac8-a53d-4d5e-b655-aac991787de0"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "206d26ab-33c5-4e4a-b79c-9271e14909ef.jpg" },
                    { new Guid("00e48016-4f19-4d14-9cb0-3d2a267c21f0"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), "aa400ebb-16f8-4eaf-89c9-0c96e7b19b3c.jpg" },
                    { new Guid("02760cb8-188e-45e7-b1d3-23915bcdf8c0"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), "3976d87d-f989-4b01-9bb4-142ce3aeeddc.jpg" },
                    { new Guid("030e64db-93ff-4fab-b16b-50677f5f1723"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), "47613ec5-8192-4e37-acd3-c2302aa54386.jpg" },
                    { new Guid("076617b2-f4bd-439e-bb57-67633ba31448"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), "40192c2c-1a68-4130-b243-1cf07cc48b47.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("0a39012b-5764-4b46-b674-4f72ea74bacb"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), true, "44811c06-278a-45e5-8411-717827a59107.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("0c665169-d7a3-4781-8ad3-b656ed553183"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "e676f25d-4eb5-4a57-a5ec-93a8b7d0c26c.jpg" },
                    { new Guid("0e33f4df-9337-4158-b444-caedb779b555"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "020a0db7-2ca2-472a-bb79-6fe49dbbb328.jpg" },
                    { new Guid("0f3000ac-68fd-46e3-b0cc-bcd40f2927bc"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "cc614324-72a9-4020-bf14-57806546f28f.jpg" },
                    { new Guid("10629003-027a-4484-befb-63ab96a1635b"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), "b84ef14b-1138-47c8-8343-e3ee9be9b82a.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("124f2af3-8b9a-4963-b272-638cd975e988"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), true, "5f37b647-e33d-440e-88ed-2e0d956f377a.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("135cab9b-66ed-47be-864a-72c17480cc7b"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), "e7cbe4a6-8ad2-4c4e-8075-02fe04a2a357.jpg" },
                    { new Guid("14937ac5-c939-4b80-bd70-3dbb5ecaac6b"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), "ba97f13b-604b-456f-b0bb-4200d2dd7995.jpg" },
                    { new Guid("1c559da5-69ea-450e-913a-ef0bb9a89a32"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), "2bd97348-3fe1-4c23-b577-bdde99af91f6.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("1c95b993-955d-45ce-853b-984a03a441f8"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), true, "f34f2a25-8e55-4826-8ce4-aca2a2a76c3a.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("1d43d597-036f-4c7c-976b-84a49d8a802a"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "87e71440-0f75-4752-9932-89875061ae2a.jpg" },
                    { new Guid("1fda0cd4-e54a-47dc-985f-55295e9d0405"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "d709d72d-e919-4e25-90e7-f7174fab8b45.jpg" },
                    { new Guid("220d183a-888b-4974-b8f6-ee506f647338"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "f2a0c878-277c-4ab7-ac8b-aee7dbf4bfa2.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("25098581-ef31-4bfb-b64b-a4dfaa6b765d"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), true, "4949849a-cc7a-4481-9c77-929fdbb71310.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("3d354b63-76ab-4129-9254-4e581144e3c9"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), "e60def66-9d33-4c35-9592-4c862a600d4c.jpg" },
                    { new Guid("3e3aaed4-9339-4a10-a999-df8584314cdc"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), "b696408f-529b-4d0b-938c-41661b206963.jpg" },
                    { new Guid("3f4df348-1950-4ce6-9d8a-3b5803cdef49"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "54b7f0e6-9115-4b2c-b0a9-37b2f049eaad.jpg" },
                    { new Guid("411e1f6a-7bb8-48bb-9a50-8de52bff30fb"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "ab6653b8-dcd2-4cf3-9402-78a29db6f26c.jpg" },
                    { new Guid("42ed369f-5ee0-407c-bda7-8994002eb8ae"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), "7c3939e2-e1d3-4486-85d6-b2c98e7e4e97.jpg" },
                    { new Guid("4415b41a-6289-4650-8e4d-30165d61ea88"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), "e61529b0-e453-4e1f-8c5c-6052b261960c.jpg" },
                    { new Guid("4467d7a3-de94-4811-a128-adaaca0d035e"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), "77153e1e-fc9b-40aa-8519-0ff9b3ed9ca0.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[,]
                {
                    { new Guid("48522876-284f-40fd-a58c-9dcf12e631e2"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), true, "5b23a8b9-9686-4107-b2ff-2a468167a32f.jpg" },
                    { new Guid("4ad0d7f1-a48b-468e-9008-9739cb88628c"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), true, "df4ae3d4-162f-4c80-85c9-c7fad7ff62b1.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[] { new Guid("4bfd8674-47b5-481a-80ca-bc07ab00d627"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "199c2fa1-560a-4a26-b0e3-7e59f5b04e9f.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("4dbf4c05-cfd0-4584-9717-2443b9dbcc38"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), true, "350540d5-80b9-49fa-8fb2-8a58c80d149c.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[] { new Guid("4e0c61df-cc97-4538-bc2e-b922d1e4e17b"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "e53b1924-4882-4551-bd05-fe72a6a7769e.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("53600cbe-3ae0-4ff2-9d9e-baba24a5a468"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), true, "a8b9cf9c-4f8e-4e18-8413-bf5de4cb4b3c.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("55ce80cb-319e-4be1-9089-361bb69ebb32"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), "9a59397d-0379-4e27-9b4d-89e2bcbbb925.jpg" },
                    { new Guid("5e3b3401-2668-4ec9-a893-ee6c535ddd78"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "82b24643-7e82-4042-be6b-d1704c537371.jpg" },
                    { new Guid("63c9b536-0377-496a-83a6-3e024092d2b7"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), "6d1f56e5-4589-4dfc-9eb9-6e8214f94551.jpg" },
                    { new Guid("67412086-3b10-4586-b0e4-928194372f6b"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), "23958beb-15a5-4ed5-a65e-6c41b9cd22fc.jpg" },
                    { new Guid("67673f47-35fd-4c14-a026-d7a61a8936ed"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "d5de5dc4-2e9c-4c2b-8284-85039169bca9.jpg" },
                    { new Guid("67d4b845-2959-41cd-ac95-b442a4e32cb5"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "cce26a3b-f6dc-47eb-a624-2bc4df0a7623.jpg" },
                    { new Guid("69353e06-bc49-40ec-a279-867b42d40b8c"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), "b5d197de-6293-4af3-9972-88549eadb931.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("6e2b4f81-65c8-43aa-923b-f140474e3fba"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), true, "b3a2b770-e6b4-44e0-aa25-15d7447d1200.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("708f771a-5a0e-4269-814c-ef8eed59f552"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), "ffd72d54-ed4c-44aa-99fe-616d5479e28d.jpg" },
                    { new Guid("714a64bf-fae0-40c6-a198-ec84d320a03c"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "60b84064-9f5b-454e-a2ea-d38c46c2f03f.jpg" },
                    { new Guid("721063ed-d498-4070-a4e1-940a9f849a14"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "3b6f886f-a15a-45ff-9644-944d0b97eaaa.jpg" },
                    { new Guid("72ce9611-d5d0-4b95-98b7-cdf95c5410c9"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), "eff526a7-d751-49d9-b0a9-cc99abcbc08d.jpg" },
                    { new Guid("73138467-d5af-499a-8624-f9d5de52a54f"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "77264134-6aab-419e-ad5d-3503697c6823.jpg" },
                    { new Guid("736a7154-cd0e-4c5d-8edb-e7541cb656df"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), "53b93388-64ee-4cf7-89fd-b0af5a446239.jpg" },
                    { new Guid("79f38fb1-39b2-4f72-a08c-16dac303f0ef"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "0785f2aa-1e76-4d82-ada6-1e70c27621e1.jpg" },
                    { new Guid("7f58809a-c241-4e3e-b841-9327111940d4"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "e10af629-01f0-41f5-b078-fe56db331999.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("86359d64-b633-4db1-8c30-04737b55cb36"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), true, "280b9c0f-257e-4f05-b2db-84e704fda33d.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("86d5507a-b6e5-445f-ae5a-748b7c755461"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), "1d5d2358-ae0c-444b-b383-7d46a28bfa66.jpg" },
                    { new Guid("8b13d878-8ebc-452e-bf29-b483f72c6887"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "0ff472a2-8be1-43a5-8671-4bf71150b036.jpg" },
                    { new Guid("8fae68b9-e4f8-420a-91a8-4fea7c80b7e0"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "b2bbcf89-6fb0-44a5-924e-6271a8f824b6.jpg" },
                    { new Guid("92c77996-58e0-44ad-b263-f02f11820a6d"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "54b08176-c485-416f-8950-9c74a5b1feea.jpg" },
                    { new Guid("93c20292-3ece-4b3a-b5d7-0eeb103f021e"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "61bf3454-723f-44f2-831e-340673d566fd.jpg" },
                    { new Guid("9b2c4cb0-59a6-41b4-84ae-0ddeee3f4e9e"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "800cffd3-c4f6-4395-b444-ff09cf93ba01.jpg" },
                    { new Guid("9ea84996-fb02-49ac-8a73-c315e1bc6abd"), new Guid("ff3ed239-e178-4632-8385-042286991c66"), "5d1e782c-7e22-4045-92bc-eb53b15164ce.jpg" },
                    { new Guid("9f54492c-1d9b-41e5-9188-b8ae001f51f8"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), "2d915949-80ea-418f-b0c0-9289649a23fb.jpg" },
                    { new Guid("a06f8821-9e20-4a89-bd19-27db1ac39f11"), new Guid("cec8f297-d65b-485a-adc3-f015139cd0c2"), "296d02ff-cc6c-4027-81d6-fa75630d9e5b.jpg" },
                    { new Guid("a08032ed-af65-4e40-a2cd-867ca6eb0532"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), "88e11d00-53a4-4ecf-ba99-c066795d2e6d.jpg" },
                    { new Guid("a1a38ff8-f33b-4d1d-a56a-d261a295d193"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), "c69fbb13-5901-41a2-99cd-7ad97aa38b71.jpg" },
                    { new Guid("a9b58e64-d69f-4c4d-91fb-4a701d0a2c21"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), "c3e0d551-cb43-4163-9653-fcdba91d4126.jpg" },
                    { new Guid("a9ceabed-f21c-4f01-9303-26852c15524a"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "91ccd7ae-1cec-4462-bae5-a0f97cc43713.jpg" },
                    { new Guid("ab5a6dbb-4967-4d68-837f-161e4a5a6cc0"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), "afcedc45-49e0-497f-8f64-a9f819427332.jpg" },
                    { new Guid("b076d62a-8371-4d03-8621-05885a55019f"), new Guid("cfa003b3-8739-415a-839f-f7ef9c86ec77"), "592aafe4-0353-4241-8c8a-174016c69eca.jpg" },
                    { new Guid("b23f5aef-aa7e-44c9-b38f-29c89fd60831"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "fbb011ba-234d-4b67-9edd-4e5a5f7c103e.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("b6b0109e-76d9-47fd-bd1d-7c18166caf41"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), true, "ec367062-0089-4605-9646-5c52baaff96e.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("bba9ae96-13a7-4e28-abcd-63f33f63a28d"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "afab16dd-fb18-4b62-ba16-bea8c4514d68.jpg" },
                    { new Guid("c507473a-9840-4fad-88bb-b7baf3a08fb4"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), "a1673708-5047-4d35-b6a1-1d46c4ea2efd.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[,]
                {
                    { new Guid("c78ffe71-6d43-40f6-a2da-481c74714868"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), true, "9b5946b1-66a5-4e0a-89fb-0c0f026c5431.jpg" },
                    { new Guid("c904526c-50f9-4bc3-9d3f-e5ce2a1d6400"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), true, "cf8e8bf0-59b5-44b2-bb5c-478b18b7f767.jpg" },
                    { new Guid("c971b3c2-9412-4b11-bf64-9c658365a05d"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), true, "10e151bb-bcf2-4b5e-9de4-b5b27fbd1036.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("cde39c7b-4cbb-45f9-86c7-8476925844ac"), new Guid("811c0a6f-5e99-4ad6-99b8-6b127cd8a665"), "13090d47-6004-41cc-a32a-adb54957c40f.jpg" },
                    { new Guid("d0fb5983-3c9a-4aea-9eca-186889cd9eb8"), new Guid("9b4668d9-563c-4699-9285-0ce01fa0e86a"), "f058dcbb-7dcc-4e48-8d89-ea439bab2584.jpg" },
                    { new Guid("d6093aca-5a60-469a-b5a8-9434d1c885b1"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), "a100ad0c-2ace-44c1-b62b-6d15323db7ed.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[,]
                {
                    { new Guid("d73e199a-4571-4f44-be14-0e21e974cad1"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), true, "93a1a227-6394-49fe-8ab9-b26b14cfd0e9.jpg" },
                    { new Guid("da5a3a00-dd62-43ff-a270-7097112aa2ce"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), true, "68233a60-a0ce-4a7c-9049-5fd6dd3981fd.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("dac61436-2392-47d6-a679-6f6d1ae94225"), new Guid("0c94ee3c-86f3-4e83-afb2-2a753416227a"), "d8c1f786-4bb3-44df-b5a8-7b806788c246.jpg" },
                    { new Guid("de4802b6-241c-4ff0-a55f-c99f4f7f2e40"), new Guid("496d119c-3629-4fba-8a34-a74d7668dd30"), "df489691-eb03-4349-b883-2d5a4dde6a5b.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("de492261-06bc-460b-8b28-47efcdc2a3d0"), new Guid("54a6579f-bfed-4f02-b30c-6bcd7931c125"), true, "68c11a26-bcf2-45d5-b1a4-0d230963fa9e.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("e20bf13c-a35a-44f6-ba11-12eca65c0366"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), "edaf24b2-0a7b-4a18-b44f-eca6ad470460.jpg" },
                    { new Guid("e343c782-8671-4eba-b282-d5fd7a86c9ab"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "0a056b0c-8418-4d1a-8a1e-fd5e366aef78.jpg" },
                    { new Guid("e83e40ee-9a82-4885-b661-6e1ecb6dadef"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "ae68d8ba-1066-4c99-b1a0-e487da50c0a6.jpg" },
                    { new Guid("ea1cd1ad-6483-46cb-8a40-b64a214b4ede"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), "16bc5db5-02da-4f1a-b7bb-2075bf721192.jpg" },
                    { new Guid("ead880ce-bc68-40ff-982c-639e96f3de15"), new Guid("62947bc9-568c-4c34-a8e1-2fb6f05bca61"), "8a18db5b-415d-4c79-ba04-f6c2bd3e534d.jpg" },
                    { new Guid("eb227c65-5393-4c68-9eab-68406b78a89d"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "f12738bf-a7f0-4dc1-a16e-90c3fc6ea823.jpg" },
                    { new Guid("ed5a4c3e-d0b2-4f75-a1e4-825ebe7a748d"), new Guid("e25b1044-5049-4ca9-954c-db76ae235862"), "39f57bd8-d2db-4d9e-8674-16d015703285.jpg" },
                    { new Guid("eef6391d-67ef-4328-850b-ae4039c6097f"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), "07ef8733-e588-48b6-bfb9-e4258e61972d.jpg" },
                    { new Guid("f1d34714-981d-47fa-8702-934af9d397fe"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), "3fc5eb25-7afd-4227-b1ea-730b2bb720a2.jpg" },
                    { new Guid("f2dfc283-587d-4fd9-a29b-599868a05bf6"), new Guid("81c17e89-5bc3-42bb-9897-ddf27d111440"), "906885f5-840b-4410-bb69-84f6b492c876.jpg" },
                    { new Guid("f3d82ae3-6eed-4cfc-a9c9-180c2cb0e8d0"), new Guid("89735a17-1395-45a9-a05a-f7d01647c329"), "3e4dca19-d4dd-437f-bc48-b6adba3f25b1.jpg" },
                    { new Guid("f42211a7-99ca-47eb-8404-c502bbc22a32"), new Guid("ecea3c10-0179-4127-8953-52b375cdca63"), "0131a704-6437-4a12-90b9-c8340ff5f434.jpg" },
                    { new Guid("f4840eb1-e492-480f-a418-5f30d68cb215"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), "69fee451-9f08-4ab1-bcfb-cf7c8068c3a4.jpg" },
                    { new Guid("f5f47f39-7d77-4d3f-93cc-81b8893637c5"), new Guid("65a26c06-6cf5-44d0-9a14-ac8fe039fd18"), "f5ed7681-ef67-463b-b248-bd546efa2851.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[,]
                {
                    { new Guid("f90c38c4-5c6f-467c-b2b1-7cc2967735c1"), new Guid("8dc3b2f9-850b-42cc-824c-7758112b9370"), true, "b33390f8-430a-456c-b821-83a8b9406043.jpg" },
                    { new Guid("f97ab381-8918-426d-b957-3953c5c107c8"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), true, "5523143a-02f3-44ef-abcd-5dca1aa1a42c.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[,]
                {
                    { new Guid("f9ae8eaf-069a-47cd-b19e-6b6e8166165e"), new Guid("bb36c355-2c8e-4a45-9be3-151934e2ff4c"), "e46dace3-f0ac-4bca-8fd1-b8e25c815472.jpg" },
                    { new Guid("f9ed17d7-65c5-479a-9e5b-1920ffc15b96"), new Guid("2d1b2f18-36ac-4cb2-acdf-354ec87a48a6"), "405ae9ad-45fe-4521-9848-321c8fb58d8b.jpg" },
                    { new Guid("faebb06d-d414-4e97-80ba-c6f1fc2ab037"), new Guid("83fe2715-9487-46b0-b5d5-eba33abd11e9"), "7fcd07d1-4b17-43f7-843e-c72f032d3915.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "IsCover", "Url" },
                values: new object[] { new Guid("fd1319bd-125b-44d7-b257-9f6b05b23a09"), new Guid("589aca9c-2b07-42d2-8920-c4406e5da977"), true, "372a18e3-7932-4ef5-8471-99ce5f3e098a.jpg" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ExperienceId", "Url" },
                values: new object[] { new Guid("fd56c8cb-ee90-4140-9134-ab759f6b5be5"), new Guid("6f236570-1625-4190-9a4f-0da2d0639386"), "26ba5e64-1634-41a0-a3de-d08ea7e86d9d.jpg" });

            migrationBuilder.InsertData(
                table: "OrderExperiences",
                columns: new[] { "OrderExperienceId", "ExperienceId", "OrderId", "Quantity", "Title", "TotalPrice", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("30f7aa04-aa62-41bc-99a7-9729a455d0a8"), null, new Guid("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"), 3, "Escursione notturna sull'Etna", 360m, 120m },
                    { new Guid("39ea9347-0d41-403a-81c5-baf69a343eb9"), null, new Guid("089b2a7e-4287-4e1c-8928-693a736db304"), 1, "Escursione in e-bike nei borghi del Montefeltro", 70m, 70m },
                    { new Guid("471f10b0-b759-49c0-b34d-aec032d163f6"), null, new Guid("dc2a8bdd-f6cc-4637-9104-639f0e020777"), 2, "Percorso benessere in grotta termale", 190m, 95m },
                    { new Guid("5bf549b8-f233-4be8-ba39-57377100149e"), null, new Guid("089b2a7e-4287-4e1c-8928-693a736db304"), 1, "Degustazione di vini in cantina sotterranea", 75m, 75m },
                    { new Guid("78bcc835-6806-4d4e-b6fb-1a8cbe0bc1c1"), null, new Guid("dc2a8bdd-f6cc-4637-9104-639f0e020777"), 1, "Trekking sul sentiero degli Dei", 65m, 65m },
                    { new Guid("99c4650a-29ea-4ae9-8c4c-26d86c4497ca"), null, new Guid("cf854aee-04c4-43ff-bb30-445daa75478a"), 2, "Cucina toscana nella tenuta di un castello", 300m, 150m },
                    { new Guid("ecc02f40-aeab-4b84-b4e9-308da99eaf22"), null, new Guid("d1f55060-cb7a-4c66-b674-adda6099dde5"), 1, "Ferrari Driving Experience a Monza", 399m, 399m },
                    { new Guid("f1b25b3f-60b1-4103-a342-76ef3346f1ed"), null, new Guid("dc2a8bdd-f6cc-4637-9104-639f0e020777"), 1, "Volo in mongolfiera al tramonto", 237.49m, 237.49m },
                    { new Guid("f9bf51b8-0db6-4f5a-86e4-454e4bba6634"), null, new Guid("d1f55060-cb7a-4c66-b674-adda6099dde5"), 1, "Rafting nelle rapide del fiume Nera", 80.75m, 80.75m }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "VoucherId", "CategoryId", "CreatedAt", "Duration", "ExpirationDate", "IsFreeCancellable", "IsUsed", "OrderId", "Organiser", "Place", "Price", "ReservationDate", "Title", "UserId", "VoucherCode" },
                values: new object[,]
                {
                    { new Guid("0931cdc6-0031-40dd-ac09-6a6d6f18a744"), new Guid("b70671a5-3989-4e7c-9cd5-c6343e09fcde"), new DateTime(2025, 1, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), "3 ore", new DateTime(2026, 1, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("dc2a8bdd-f6cc-4637-9104-639f0e020777"), "Terme Naturali", "Saturnia, Toscana", 95m, new DateTime(2025, 8, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), "Percorso benessere in grotta termale", "698c347e-bb57-4cb4-b672-9940647f250d", "234DJD37-CMZNXS23-SDER456P" },
                    { new Guid("292c0ec5-a9a7-4484-8b6a-b42ae494a2c7"), new Guid("1652310e-b8f3-43e7-bd9d-287f73f939b5"), new DateTime(2025, 2, 23, 11, 0, 56, 0, DateTimeKind.Unspecified), "6 ore", new DateTime(2027, 2, 23, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"), "Sicilia Avventure", "Catania, Sicilia", 120m, new DateTime(2026, 6, 21, 11, 0, 56, 0, DateTimeKind.Unspecified), "Escursione notturna sull'Etna", "698c347e-bb57-4cb4-b672-9940647f250d", "MCNXVAST-234756DF-CGDETQ09" },
                    { new Guid("44f90443-8008-4cb5-87a6-074388ad37a6"), new Guid("b70671a5-3989-4e7c-9cd5-c6343e09fcde"), new DateTime(2025, 1, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), "3 ore", new DateTime(2026, 1, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("dc2a8bdd-f6cc-4637-9104-639f0e020777"), "Terme Naturali", "Saturnia, Toscana", 95m, new DateTime(2025, 8, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), "Percorso benessere in grotta termale", "698c347e-bb57-4cb4-b672-9940647f250d", "A10CVD32-XCMZ12WE-CDC34509" },
                    { new Guid("5885d06b-8a41-4e7d-994a-9c87a7133a53"), new Guid("bda4ee71-af9c-46c6-b1bf-95f178773a2f"), new DateTime(2025, 1, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), "6 ore", new DateTime(2025, 7, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("dc2a8bdd-f6cc-4637-9104-639f0e020777"), "Italia Escursioni", "Costiera Amalfitana, Campania", 65m, new DateTime(2025, 4, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), "Trekking sul sentiero degli Dei", "698c347e-bb57-4cb4-b672-9940647f250d", "POET3512-DCF456WR-ASVCTE76" },
                    { new Guid("6e12c524-e60d-4feb-8f49-29c9fd71fc21"), new Guid("1652310e-b8f3-43e7-bd9d-287f73f939b5"), new DateTime(2025, 2, 23, 11, 0, 56, 0, DateTimeKind.Unspecified), "6 ore", new DateTime(2027, 2, 23, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"), "Sicilia Avventure", "Catania, Sicilia", 120m, new DateTime(2026, 6, 21, 11, 0, 56, 0, DateTimeKind.Unspecified), "Escursione notturna sull'Etna", "698c347e-bb57-4cb4-b672-9940647f250d", "SPE46572-CXASWE12-CMNZGHD4" },
                    { new Guid("70168df0-0dd4-45db-933a-5edd4356865e"), new Guid("da780fcf-074e-4e0c-b0b8-1bd8e0c0fa6f"), new DateTime(2025, 1, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), "3 ore", new DateTime(2025, 9, 12, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("dc2a8bdd-f6cc-4637-9104-639f0e020777"), "Avventure Italiane", "Siena, Toscana", 249.99m, new DateTime(2025, 8, 8, 11, 0, 56, 0, DateTimeKind.Unspecified), "Volo in mongolfiera al tramonto", "698c347e-bb57-4cb4-b672-9940647f250d", "123POERT-45RT653M-CMPR503I" },
                    { new Guid("af5e7042-c9c7-4535-b3f6-d1956eb25441"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), new DateTime(2024, 2, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "2 ore", new DateTime(2025, 2, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("089b2a7e-4287-4e1c-8928-693a736db304"), "Cantine Toscane", "Montepulciano, Toscana", 75m, new DateTime(2024, 4, 17, 11, 0, 56, 0, DateTimeKind.Unspecified), "Degustazione di vini in cantina sotterranea", "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2", "ER57HF75-RTIP39E8-WE3210PQ" },
                    { new Guid("bb706e8b-7647-431d-ab27-99572fb64415"), new Guid("6f3a957c-df09-437c-bc37-f069173eabe2"), new DateTime(2025, 4, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), "2 ore", new DateTime(2027, 4, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("d1f55060-cb7a-4c66-b674-adda6099dde5"), "Motor Experience", "Monza, Lombardia", 399m, new DateTime(2026, 7, 15, 11, 0, 56, 0, DateTimeKind.Unspecified), "Ferrari Driving Experience a Monza", "698c347e-bb57-4cb4-b672-9940647f250d", "QWE23CDT-VBHGDTWQ-ZSDE125E" },
                    { new Guid("bbc9cf87-e09b-4e31-9b82-6d7086c199d0"), new Guid("6accf29d-8d1c-4edd-b48a-c70251516b99"), new DateTime(2025, 4, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), "4 ore", new DateTime(2026, 4, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("d1f55060-cb7a-4c66-b674-adda6099dde5"), "Avventure Italiane", "Scheggino, Umbria", 80.75m, new DateTime(2025, 7, 23, 11, 0, 56, 0, DateTimeKind.Unspecified), "Rafting nelle rapide del fiume Nera", "698c347e-bb57-4cb4-b672-9940647f250d", "4563HDGR-CV6S7E34-VBXNAJEI" },
                    { new Guid("c5a9d186-ec49-4568-9a87-a5832ce2957f"), new Guid("1652310e-b8f3-43e7-bd9d-287f73f939b5"), new DateTime(2025, 2, 23, 11, 0, 56, 0, DateTimeKind.Unspecified), "6 ore", new DateTime(2027, 2, 23, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("1baad7eb-e2a6-45d9-bf8c-e68579cedfd6"), "Sicilia Avventure", "Catania, Sicilia", 120m, new DateTime(2026, 6, 21, 11, 0, 56, 0, DateTimeKind.Unspecified), "Escursione notturna sull'Etna", "698c347e-bb57-4cb4-b672-9940647f250d", "POWE3456-CMZXNCE5-CMV45012" },
                    { new Guid("e6ebc89b-fefb-43ab-a8e0-df2a48fbe369"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), new DateTime(2024, 7, 25, 11, 0, 56, 0, DateTimeKind.Unspecified), "5 ore", new DateTime(2025, 7, 25, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("cf854aee-04c4-43ff-bb30-445daa75478a"), "Sapori d'Italia", "Chianti, Toscana", 150m, new DateTime(2025, 4, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), "Cucina toscana nella tenuta di un castello", "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2", "QW12EDCM-SX2091QS-1AZWE937" },
                    { new Guid("f1dfe658-59e3-4917-adf1-99a7aae42cd1"), new Guid("5fdffa0f-a615-43f2-aa15-88bc8dcec27f"), new DateTime(2024, 7, 25, 11, 0, 56, 0, DateTimeKind.Unspecified), "5 ore", new DateTime(2025, 7, 25, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("cf854aee-04c4-43ff-bb30-445daa75478a"), "Sapori d'Italia", "Chianti, Toscana", 150m, new DateTime(2025, 4, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), "Cucina toscana nella tenuta di un castello", "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2", "SZMAPQ92-AXQW1200-QASW34FR" },
                    { new Guid("fef8e669-b3b7-4ca7-90fd-270465381881"), new Guid("a4049ef8-1e86-48bf-b514-3930469ddcbd"), new DateTime(2024, 2, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), "7 ore", new DateTime(2025, 2, 9, 11, 0, 56, 0, DateTimeKind.Unspecified), true, true, new Guid("089b2a7e-4287-4e1c-8928-693a736db304"), "Marche Experience", "Urbino, Marche", 70m, new DateTime(2025, 1, 28, 11, 0, 56, 0, DateTimeKind.Unspecified), "Escursione in e-bike nei borghi del Montefeltro", "21f6b4b5-9616-4380-a9d3-3ddb2f4b72c2", "34FR4R5T-FR56TY5I-DRIUE321" }
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
                name: "IX_Coupons_UserId",
                table: "Coupons",
                column: "UserId");

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
                name: "IX_FidelityCards_CardNumber",
                table: "FidelityCards",
                column: "CardNumber",
                unique: true,
                filter: "[CardNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FidelityCards_UserId",
                table: "FidelityCards",
                column: "UserId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_CategoryId",
                table: "Vouchers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_OrderId",
                table: "Vouchers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_UserId",
                table: "Vouchers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_VoucherCode",
                table: "Vouchers",
                column: "VoucherCode",
                unique: true,
                filter: "[VoucherCode] IS NOT NULL");
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
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "FidelityCards");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "OrderExperiences");

            migrationBuilder.DropTable(
                name: "Vouchers");

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
