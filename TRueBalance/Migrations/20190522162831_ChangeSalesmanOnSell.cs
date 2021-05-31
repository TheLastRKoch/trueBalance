using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TRueBalance.Migrations
{
    public partial class ChangeSalesmanOnSell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salesman",
                table: "Sells");

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "Sells",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "Sells");

            migrationBuilder.AddColumn<string>(
                name: "Salesman",
                table: "Sells",
                nullable: true);
        }
    }
}
