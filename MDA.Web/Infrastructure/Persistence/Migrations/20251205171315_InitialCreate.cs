using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDA.Web.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IbkrAccountId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    BaseCurrency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    AccountType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    LastUpdatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalPortfolioValue = table.Column<decimal>(type: "numeric", nullable: true),
                    SettledCash = table.Column<decimal>(type: "numeric", nullable: true),
                    ExcessLiquidity = table.Column<decimal>(type: "numeric", nullable: true),
                    BuyingPower = table.Column<decimal>(type: "numeric", nullable: true),
                    DailyPnl = table.Column<decimal>(type: "numeric", nullable: true),
                    UnrealizedPnl = table.Column<decimal>(type: "numeric", nullable: true),
                    ExpectedDividendsYear = table.Column<decimal>(type: "numeric", nullable: true),
                    DividendsPaidYtd = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Symbol = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IbkrConId = table.Column<int>(type: "integer", nullable: false),
                    AssetClass = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    LastPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    LastPriceUpdatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Pe = table.Column<decimal>(type: "numeric", nullable: true),
                    ForwardPe = table.Column<decimal>(type: "numeric", nullable: true),
                    Week52High = table.Column<decimal>(type: "numeric", nullable: true),
                    Ath = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Horizon = table.Column<int>(type: "integer", nullable: false),
                    TargetWeightPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryGoals_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holdings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstrumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    UnrealizedPNL = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    RealizedPNL = table.Column<decimal>(type: "numeric(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holdings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holdings_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Holdings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Holdings_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountType", "BaseCurrency", "BuyingPower", "DailyPnl", "DividendsPaidYtd", "ExcessLiquidity", "ExpectedDividendsYear", "IbkrAccountId", "LastUpdatedUtc", "SettledCash", "TotalPortfolioValue", "UnrealizedPnl", "UserId" },
                values: new object[] { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "INDIVIDUAL", "USD", null, null, null, null, null, "U8515462", null, null, null, null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AccountId",
                table: "Categories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGoals_CategoryId",
                table: "CategoryGoals",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Holdings_AccountId",
                table: "Holdings",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Holdings_CategoryId",
                table: "Holdings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Holdings_InstrumentId",
                table: "Holdings",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_IbkrConId",
                table: "Instruments",
                column: "IbkrConId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGoals");

            migrationBuilder.DropTable(
                name: "Holdings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
