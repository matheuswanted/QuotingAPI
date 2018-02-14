using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Quoting.Infrastructure.Migrations
{
    public partial class quoteRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "seq_quote_requests",
                incrementBy: 10);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Quotes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuoteRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Customer_Address = table.Column<string>(nullable: true),
                    Customer_BirthDate = table.Column<DateTime>(nullable: false),
                    Customer_Email = table.Column<string>(nullable: true),
                    Customer_Gender = table.Column<string>(nullable: true),
                    Customer_Phone = table.Column<string>(nullable: true),
                    Customer_SSN = table.Column<string>(nullable: true),
                    Vehicle_Make = table.Column<string>(nullable: true),
                    Vehicle_ManufacturingYear = table.Column<int>(nullable: false),
                    Vehicle_Model = table.Column<string>(nullable: true),
                    Vehicle_Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_RequestId",
                table: "Quotes",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_QuoteRequests_RequestId",
                table: "Quotes",
                column: "RequestId",
                principalTable: "QuoteRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_QuoteRequests_RequestId",
                table: "Quotes");

            migrationBuilder.DropTable(
                name: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_RequestId",
                table: "Quotes");

            migrationBuilder.DropSequence(
                name: "seq_quote_requests");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Quotes");
        }
    }
}
