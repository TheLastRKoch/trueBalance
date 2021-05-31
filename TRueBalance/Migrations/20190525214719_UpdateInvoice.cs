using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TRueBalance.Migrations
{
    public partial class UpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "PrintQueues");

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "Sells",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "PrintQueues",
                nullable: true);
        }
    }
}
