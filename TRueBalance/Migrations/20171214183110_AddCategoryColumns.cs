using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TRueBalance.Migrations
{
    public partial class AddCategoryColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Meals_MealID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_UserSettings_UserSettingSettingID",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MealID",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserSettingSettingID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MealID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserSettingSettingID",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "UserSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Meals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_CategoryID",
                table: "UserSettings",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_CategoryID",
                table: "Meals",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Categories_CategoryID",
                table: "Meals",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_Categories_CategoryID",
                table: "UserSettings",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Categories_CategoryID",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_Categories_CategoryID",
                table: "UserSettings");

            migrationBuilder.DropIndex(
                name: "IX_UserSettings_CategoryID",
                table: "UserSettings");

            migrationBuilder.DropIndex(
                name: "IX_Meals_CategoryID",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Meals");

            migrationBuilder.AddColumn<int>(
                name: "MealID",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserSettingSettingID",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MealID",
                table: "Categories",
                column: "MealID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserSettingSettingID",
                table: "Categories",
                column: "UserSettingSettingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Meals_MealID",
                table: "Categories",
                column: "MealID",
                principalTable: "Meals",
                principalColumn: "MealID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_UserSettings_UserSettingSettingID",
                table: "Categories",
                column: "UserSettingSettingID",
                principalTable: "UserSettings",
                principalColumn: "SettingID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
