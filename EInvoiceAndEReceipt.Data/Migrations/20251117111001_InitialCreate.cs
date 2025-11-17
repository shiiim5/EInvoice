using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EInvoiceAndEReceipt.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delievries",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Approach = table.Column<string>(type: "text", nullable: true),
                    Packaging = table.Column<string>(type: "text", nullable: true),
                    DateValidity = table.Column<string>(type: "text", nullable: true),
                    ExportPort = table.Column<string>(type: "text", nullable: true),
                    CountryOfOrigin = table.Column<string>(type: "text", nullable: true),
                    GrossWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    NetWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    Terms = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delievries", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "IssuerAddresses",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchId = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Governate = table.Column<string>(type: "text", nullable: false),
                    RegionCity = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    BuildingNumber = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    Floor = table.Column<string>(type: "text", nullable: true),
                    Room = table.Column<string>(type: "text", nullable: true),
                    LandMark = table.Column<string>(type: "text", nullable: true),
                    AdditionalInformation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuerAddresses", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BankName = table.Column<string>(type: "text", nullable: true),
                    BankAddress = table.Column<string>(type: "text", nullable: true),
                    BankAccountNo = table.Column<string>(type: "text", nullable: true),
                    BankAccountIBAN = table.Column<string>(type: "text", nullable: true),
                    SwiftCode = table.Column<string>(type: "text", nullable: true),
                    Terms = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiverAddresses",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Governate = table.Column<string>(type: "text", nullable: false),
                    RegionCity = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    BuildingNumber = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    Floor = table.Column<string>(type: "text", nullable: true),
                    Room = table.Column<string>(type: "text", nullable: true),
                    LandMark = table.Column<string>(type: "text", nullable: true),
                    AdditionalInformation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiverAddresses", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrencySold = table.Column<string>(type: "text", nullable: false),
                    AmountEGP = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountSold = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrencyExchangeRate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "Issuers",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuers", x => x._Id);
                    table.ForeignKey(
                        name: "FK_Issuers_IssuerAddresses_Address_Id",
                        column: x => x.Address_Id,
                        principalTable: "IssuerAddresses",
                        principalColumn: "_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receivers",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivers", x => x._Id);
                    table.ForeignKey(
                        name: "FK_Receivers_IssuerAddresses_Address_Id",
                        column: x => x.Address_Id,
                        principalTable: "IssuerAddresses",
                        principalColumn: "_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Issuer_Id = table.Column<int>(type: "integer", nullable: false),
                    Receiver_Id = table.Column<int>(type: "integer", nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    DocumentTypeVersion = table.Column<string>(type: "text", nullable: false),
                    DateTimeIssued = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaxpayerActivityCode = table.Column<string>(type: "text", nullable: false),
                    InternalId = table.Column<string>(type: "text", nullable: false),
                    PurchaseOrderReference = table.Column<string>(type: "text", nullable: true),
                    PurchaseOrderDescription = table.Column<string>(type: "text", nullable: true),
                    SalesOrderReference = table.Column<string>(type: "text", nullable: true),
                    SalesOrderDescription = table.Column<string>(type: "text", nullable: true),
                    ProformaInvoiceNumber = table.Column<string>(type: "text", nullable: true),
                    Payment_Id = table.Column<int>(type: "integer", nullable: true),
                    Delievry_Id = table.Column<int>(type: "integer", nullable: true),
                    TotalSalesAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    ExtraDiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalItemsDiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    ServiceDeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x._Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Delievries_Delievry_Id",
                        column: x => x.Delievry_Id,
                        principalTable: "Delievries",
                        principalColumn: "_Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Issuers_Issuer_Id",
                        column: x => x.Issuer_Id,
                        principalTable: "Issuers",
                        principalColumn: "_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Payments_Payment_Id",
                        column: x => x.Payment_Id,
                        principalTable: "Payments",
                        principalColumn: "_Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Receivers_Receiver_Id",
                        column: x => x.Receiver_Id,
                        principalTable: "Receivers",
                        principalColumn: "_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    ItemCode = table.Column<string>(type: "text", nullable: false),
                    UnitType = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitValue_Id = table.Column<int>(type: "integer", nullable: false),
                    SalesTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    ValueDifference = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalTaxableFees = table.Column<decimal>(type: "numeric", nullable: false),
                    NetTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    ItemsDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    Discount_Id = table.Column<int>(type: "integer", nullable: true),
                    InternalCode = table.Column<string>(type: "text", nullable: true),
                    Invoice_Id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLines", x => x._Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Discounts_Discount_Id",
                        column: x => x.Discount_Id,
                        principalTable: "Discounts",
                        principalColumn: "_Id");
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Invoices_Invoice_Id",
                        column: x => x.Invoice_Id,
                        principalTable: "Invoices",
                        principalColumn: "_Id");
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Values_UnitValue_Id",
                        column: x => x.UnitValue_Id,
                        principalTable: "Values",
                        principalColumn: "_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Signatures",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Invoice_Id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signatures", x => x._Id);
                    table.ForeignKey(
                        name: "FK_Signatures_Invoices_Invoice_Id",
                        column: x => x.Invoice_Id,
                        principalTable: "Invoices",
                        principalColumn: "_Id");
                });

            migrationBuilder.CreateTable(
                name: "TaxTotals",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaxType = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Invoice_Id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTotals", x => x._Id);
                    table.ForeignKey(
                        name: "FK_TaxTotals_Invoices_Invoice_Id",
                        column: x => x.Invoice_Id,
                        principalTable: "Invoices",
                        principalColumn: "_Id");
                });

            migrationBuilder.CreateTable(
                name: "TaxableItems",
                columns: table => new
                {
                    _Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaxType = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    SubType = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false),
                    InvoiceLine_Id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxableItems", x => x._Id);
                    table.ForeignKey(
                        name: "FK_TaxableItems_InvoiceLines_InvoiceLine_Id",
                        column: x => x.InvoiceLine_Id,
                        principalTable: "InvoiceLines",
                        principalColumn: "_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_Discount_Id",
                table: "InvoiceLines",
                column: "Discount_Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_Invoice_Id",
                table: "InvoiceLines",
                column: "Invoice_Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_UnitValue_Id",
                table: "InvoiceLines",
                column: "UnitValue_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Delievry_Id",
                table: "Invoices",
                column: "Delievry_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Issuer_Id",
                table: "Invoices",
                column: "Issuer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Payment_Id",
                table: "Invoices",
                column: "Payment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Receiver_Id",
                table: "Invoices",
                column: "Receiver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Issuers_Address_Id",
                table: "Issuers",
                column: "Address_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_Address_Id",
                table: "Receivers",
                column: "Address_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_Invoice_Id",
                table: "Signatures",
                column: "Invoice_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaxableItems_InvoiceLine_Id",
                table: "TaxableItems",
                column: "InvoiceLine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaxTotals_Invoice_Id",
                table: "TaxTotals",
                column: "Invoice_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiverAddresses");

            migrationBuilder.DropTable(
                name: "Signatures");

            migrationBuilder.DropTable(
                name: "TaxableItems");

            migrationBuilder.DropTable(
                name: "TaxTotals");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Delievries");

            migrationBuilder.DropTable(
                name: "Issuers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Receivers");

            migrationBuilder.DropTable(
                name: "IssuerAddresses");
        }
    }
}
