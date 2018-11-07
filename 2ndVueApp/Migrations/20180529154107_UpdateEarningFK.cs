using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace _2ndVueApp.Migrations
{
    public partial class UpdateEarningFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Earnings_Earners_EarnerId",
                table: "Earnings");

            migrationBuilder.DropForeignKey(
                name: "FK_Earnings_Rules_RuleId",
                table: "Earnings");

            migrationBuilder.AlterColumn<int>(
                name: "RuleId",
                table: "Earnings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EarnerId",
                table: "Earnings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Earnings_Earners_EarnerId",
                table: "Earnings",
                column: "EarnerId",
                principalTable: "Earners",
                principalColumn: "EarnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Earnings_Rules_RuleId",
                table: "Earnings",
                column: "RuleId",
                principalTable: "Rules",
                principalColumn: "RuleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Earnings_Earners_EarnerId",
                table: "Earnings");

            migrationBuilder.DropForeignKey(
                name: "FK_Earnings_Rules_RuleId",
                table: "Earnings");

            migrationBuilder.AlterColumn<int>(
                name: "RuleId",
                table: "Earnings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EarnerId",
                table: "Earnings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Earnings_Earners_EarnerId",
                table: "Earnings",
                column: "EarnerId",
                principalTable: "Earners",
                principalColumn: "EarnerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Earnings_Rules_RuleId",
                table: "Earnings",
                column: "RuleId",
                principalTable: "Rules",
                principalColumn: "RuleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
