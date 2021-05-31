using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TRueBalance.Migrations
{
    public partial class AddCategoriesToOrdersTakeAway : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Takeaway",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "TakeawayCategoryID",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeCategoryID",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TakeawayCategoryID",
                table: "Orders",
                column: "TakeawayCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTypeCategoryID",
                table: "Invoices",
                column: "PaymentTypeCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Categories_PaymentTypeCategoryID",
                table: "Invoices",
                column: "PaymentTypeCategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Categories_TakeawayCategoryID",
                table: "Orders",
                column: "TakeawayCategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Categories_PaymentTypeCategoryID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Categories_TakeawayCategoryID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TakeawayCategoryID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PaymentTypeCategoryID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TakeawayCategoryID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PaymentTypeCategoryID",
                table: "Invoices");

            migrationBuilder.AddColumn<short>(
                name: "Takeaway",
                table: "Orders",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Invoices",
                nullable: true);
        }
    }
}
