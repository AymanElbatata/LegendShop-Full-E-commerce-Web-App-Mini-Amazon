using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AymanStore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppErrorTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppErrorTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CountryTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GenderTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceRateTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VATRate = table.Column<int>(type: "int", nullable: true),
                    ProfitRate = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRateTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturerTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturerTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingCompanyTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCompanyTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingServiceTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingServiceTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingStatusTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingStatusTBLs", x => x.ID);
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
                name: "SubCategoryTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryTBLId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubCategoryTBLs_CategoryTBLs_CategoryTBLId",
                        column: x => x.CategoryTBLId,
                        principalTable: "CategoryTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryTBLId = table.Column<int>(type: "int", nullable: true),
                    GenderTBLId = table.Column<int>(type: "int", nullable: true),
                    DateOfJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_CountryTBLs_CountryTBLId",
                        column: x => x.CountryTBLId,
                        principalTable: "CountryTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_GenderTBLs_GenderTBLId",
                        column: x => x.GenderTBLId,
                        principalTable: "GenderTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ShippingCompanyCostTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingCompanyTBLId = table.Column<int>(type: "int", nullable: true),
                    CountryTBLSendFromId = table.Column<int>(type: "int", nullable: true),
                    CountryTBLSendToId = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCompanyCostTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShippingCompanyCostTBLs_CountryTBLs_CountryTBLSendFromId",
                        column: x => x.CountryTBLSendFromId,
                        principalTable: "CountryTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShippingCompanyCostTBLs_CountryTBLs_CountryTBLSendToId",
                        column: x => x.CountryTBLSendToId,
                        principalTable: "CountryTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShippingCompanyCostTBLs_ShippingCompanyTBLs_ShippingCompanyTBLId",
                        column: x => x.ShippingCompanyTBLId,
                        principalTable: "ShippingCompanyTBLs",
                        principalColumn: "ID");
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
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "OrderTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderByUserTBLId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CountryTBLId = table.Column<int>(type: "int", nullable: true),
                    ShippingServiceTBLId = table.Column<int>(type: "int", nullable: true),
                    ShippingStatusTBLId = table.Column<int>(type: "int", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientPhones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsСurrentOrder = table.Column<bool>(type: "bit", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingShippingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingShippingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfSubmit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderTBLs_AspNetUsers_OrderByUserTBLId",
                        column: x => x.OrderByUserTBLId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderTBLs_CountryTBLs_CountryTBLId",
                        column: x => x.CountryTBLId,
                        principalTable: "CountryTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderTBLs_ShippingServiceTBLs_ShippingServiceTBLId",
                        column: x => x.ShippingServiceTBLId,
                        principalTable: "ShippingServiceTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderTBLs_ShippingStatusTBLs_ShippingStatusTBLId",
                        column: x => x.ShippingStatusTBLId,
                        principalTable: "ShippingStatusTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierTBLId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubCategoryTBLId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerTBLId = table.Column<int>(type: "int", nullable: true),
                    CountryTBLPlaceId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellingPrice = table.Column<int>(type: "int", nullable: true),
                    PurchasingPrice = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductTBLs_AspNetUsers_SupplierTBLId",
                        column: x => x.SupplierTBLId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTBLs_CountryTBLs_CountryTBLPlaceId",
                        column: x => x.CountryTBLPlaceId,
                        principalTable: "CountryTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductTBLs_ManufacturerTBLs_ManufacturerTBLId",
                        column: x => x.ManufacturerTBLId,
                        principalTable: "ManufacturerTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductTBLs_SubCategoryTBLs_SubCategoryTBLId",
                        column: x => x.SubCategoryTBLId,
                        principalTable: "SubCategoryTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ContactForProductErrorTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTBLId = table.Column<int>(type: "int", nullable: true),
                    SenderUserTBLId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageReplyToClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSeenMessageReplyToClient = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactForProductErrorTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactForProductErrorTBLs_AspNetUsers_SenderUserTBLId",
                        column: x => x.SenderUserTBLId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContactForProductErrorTBLs_ProductTBLs_ProductTBLId",
                        column: x => x.ProductTBLId,
                        principalTable: "ProductTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderTBLId = table.Column<int>(type: "int", nullable: true),
                    ProductTBLId = table.Column<int>(type: "int", nullable: true),
                    ShippingStatusTBLId = table.Column<int>(type: "int", nullable: true),
                    ShippingCompanyCostTBLId = table.Column<int>(type: "int", nullable: true),
                    ShippingCompanyTRKCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierPhones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUserWroteReview = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ShippingCost = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderDetailTBLs_OrderTBLs_OrderTBLId",
                        column: x => x.OrderTBLId,
                        principalTable: "OrderTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderDetailTBLs_ProductTBLs_ProductTBLId",
                        column: x => x.ProductTBLId,
                        principalTable: "ProductTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderDetailTBLs_ShippingCompanyCostTBLs_ShippingCompanyCostTBLId",
                        column: x => x.ShippingCompanyCostTBLId,
                        principalTable: "ShippingCompanyCostTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderDetailTBLs_ShippingStatusTBLs_ShippingStatusTBLId",
                        column: x => x.ShippingStatusTBLId,
                        principalTable: "ShippingStatusTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductPhotoTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTBLId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPhotoTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductPhotoTBLs_ProductTBLs_ProductTBLId",
                        column: x => x.ProductTBLId,
                        principalTable: "ProductTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductRatingTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTBLId = table.Column<int>(type: "int", nullable: true),
                    AppUserWhoRatedId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    ReviewSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHelpful = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatingTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductRatingTBLs_AspNetUsers_AppUserWhoRatedId",
                        column: x => x.AppUserWhoRatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductRatingTBLs_ProductTBLs_ProductTBLId",
                        column: x => x.ProductTBLId,
                        principalTable: "ProductTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecificationTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTBLId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecificationTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductSpecificationTBLs_ProductTBLs_ProductTBLId",
                        column: x => x.ProductTBLId,
                        principalTable: "ProductTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceOrderDetailTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailTBLId = table.Column<int>(type: "int", nullable: true),
                    IsSupplierReceivedMoney = table.Column<bool>(type: "bit", nullable: false),
                    SupplierAmount = table.Column<int>(type: "int", nullable: true),
                    VATAmount = table.Column<int>(type: "int", nullable: true),
                    ProfitAmount = table.Column<int>(type: "int", nullable: true),
                    ShippingAmount = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceOrderDetailTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvoiceOrderDetailTBLs_OrderDetailTBLs_OrderDetailTBLId",
                        column: x => x.OrderDetailTBLId,
                        principalTable: "OrderDetailTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AbuseProductRatingTBLs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductRatingTBLId = table.Column<int>(type: "int", nullable: true),
                    SenderUserTBLId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbuseProductRatingTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AbuseProductRatingTBLs_AspNetUsers_SenderUserTBLId",
                        column: x => x.SenderUserTBLId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AbuseProductRatingTBLs_ProductRatingTBLs_ProductRatingTBLId",
                        column: x => x.ProductRatingTBLId,
                        principalTable: "ProductRatingTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbuseProductRatingTBLs_ProductRatingTBLId",
                table: "AbuseProductRatingTBLs",
                column: "ProductRatingTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_AbuseProductRatingTBLs_SenderUserTBLId",
                table: "AbuseProductRatingTBLs",
                column: "SenderUserTBLId");

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
                name: "IX_AspNetUsers_CountryTBLId",
                table: "AspNetUsers",
                column: "CountryTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenderTBLId",
                table: "AspNetUsers",
                column: "GenderTBLId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContactForProductErrorTBLs_ProductTBLId",
                table: "ContactForProductErrorTBLs",
                column: "ProductTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactForProductErrorTBLs_SenderUserTBLId",
                table: "ContactForProductErrorTBLs",
                column: "SenderUserTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOrderDetailTBLs_OrderDetailTBLId",
                table: "InvoiceOrderDetailTBLs",
                column: "OrderDetailTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailTBLs_OrderTBLId",
                table: "OrderDetailTBLs",
                column: "OrderTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailTBLs_ProductTBLId",
                table: "OrderDetailTBLs",
                column: "ProductTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailTBLs_ShippingCompanyCostTBLId",
                table: "OrderDetailTBLs",
                column: "ShippingCompanyCostTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailTBLs_ShippingStatusTBLId",
                table: "OrderDetailTBLs",
                column: "ShippingStatusTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTBLs_CountryTBLId",
                table: "OrderTBLs",
                column: "CountryTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTBLs_OrderByUserTBLId",
                table: "OrderTBLs",
                column: "OrderByUserTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTBLs_ShippingServiceTBLId",
                table: "OrderTBLs",
                column: "ShippingServiceTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTBLs_ShippingStatusTBLId",
                table: "OrderTBLs",
                column: "ShippingStatusTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhotoTBLs_ProductTBLId",
                table: "ProductPhotoTBLs",
                column: "ProductTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatingTBLs_AppUserWhoRatedId",
                table: "ProductRatingTBLs",
                column: "AppUserWhoRatedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatingTBLs_ProductTBLId",
                table: "ProductRatingTBLs",
                column: "ProductTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationTBLs_ProductTBLId",
                table: "ProductSpecificationTBLs",
                column: "ProductTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTBLs_CountryTBLPlaceId",
                table: "ProductTBLs",
                column: "CountryTBLPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTBLs_ManufacturerTBLId",
                table: "ProductTBLs",
                column: "ManufacturerTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTBLs_SubCategoryTBLId",
                table: "ProductTBLs",
                column: "SubCategoryTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTBLs_SupplierTBLId",
                table: "ProductTBLs",
                column: "SupplierTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCompanyCostTBLs_CountryTBLSendFromId",
                table: "ShippingCompanyCostTBLs",
                column: "CountryTBLSendFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCompanyCostTBLs_CountryTBLSendToId",
                table: "ShippingCompanyCostTBLs",
                column: "CountryTBLSendToId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCompanyCostTBLs_ShippingCompanyTBLId",
                table: "ShippingCompanyCostTBLs",
                column: "ShippingCompanyTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryTBLs_CategoryTBLId",
                table: "SubCategoryTBLs",
                column: "CategoryTBLId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbuseProductRatingTBLs");

            migrationBuilder.DropTable(
                name: "AppErrorTBLs");

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
                name: "ContactForProductErrorTBLs");

            migrationBuilder.DropTable(
                name: "ContactUsTBLs");

            migrationBuilder.DropTable(
                name: "EmailTBLs");

            migrationBuilder.DropTable(
                name: "InvoiceOrderDetailTBLs");

            migrationBuilder.DropTable(
                name: "InvoiceRateTBLs");

            migrationBuilder.DropTable(
                name: "ProductPhotoTBLs");

            migrationBuilder.DropTable(
                name: "ProductSpecificationTBLs");

            migrationBuilder.DropTable(
                name: "ProductRatingTBLs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OrderDetailTBLs");

            migrationBuilder.DropTable(
                name: "OrderTBLs");

            migrationBuilder.DropTable(
                name: "ProductTBLs");

            migrationBuilder.DropTable(
                name: "ShippingCompanyCostTBLs");

            migrationBuilder.DropTable(
                name: "ShippingServiceTBLs");

            migrationBuilder.DropTable(
                name: "ShippingStatusTBLs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ManufacturerTBLs");

            migrationBuilder.DropTable(
                name: "SubCategoryTBLs");

            migrationBuilder.DropTable(
                name: "ShippingCompanyTBLs");

            migrationBuilder.DropTable(
                name: "CountryTBLs");

            migrationBuilder.DropTable(
                name: "GenderTBLs");

            migrationBuilder.DropTable(
                name: "CategoryTBLs");
        }
    }
}
