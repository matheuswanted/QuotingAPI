using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Quoting.Infrastructure.Migrations
{
    public partial class RulesQuoteStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Customers_CustomerId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_CustomerId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_VehicleId",
                table: "Quotes");

            migrationBuilder.CreateSequence(
                name: "seq_base_price_rules",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "seq_price_modifier_rules",
                incrementBy: 10);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Quotes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Quotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Quotes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "BasePriceRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BasePrice = table.Column<decimal>(nullable: false),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePriceRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceModifierRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Modifier = table.Column<decimal>(nullable: false),
                    AgeRange_End = table.Column<int>(nullable: true),
                    AgeRange_Start = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceModifierRules", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_CustomerId",
                table: "Quotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_VehicleId",
                table: "Quotes",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Customers_CustomerId",
                table: "Quotes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Customers_CustomerId",
                table: "Quotes");

            migrationBuilder.DropTable(
                name: "BasePriceRules");

            migrationBuilder.DropTable(
                name: "PriceModifierRules");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_CustomerId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_VehicleId",
                table: "Quotes");

            migrationBuilder.DropSequence(
                name: "seq_base_price_rules");

            migrationBuilder.DropSequence(
                name: "seq_price_modifier_rules");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Quotes");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Quotes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_CustomerId",
                table: "Quotes",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_VehicleId",
                table: "Quotes",
                column: "VehicleId",
                unique: true,
                filter: "[VehicleId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Customers_CustomerId",
                table: "Quotes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
