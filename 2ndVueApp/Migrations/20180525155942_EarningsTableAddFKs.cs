using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace _2ndVueApp.Migrations
{
    public partial class EarningsTableAddFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RuleId",
                table: "Earnings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EarnerId",
                table: "Earnings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Earnings_EarnerId",
                table: "Earnings",
                column: "EarnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Earnings_RuleId",
                table: "Earnings",
                column: "RuleId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Earnings_Earners_EarnerId",
                table: "Earnings");

            migrationBuilder.DropForeignKey(
                name: "FK_Earnings_Rules_RuleId",
                table: "Earnings");

            migrationBuilder.DropIndex(
                name: "IX_Earnings_EarnerId",
                table: "Earnings");

            migrationBuilder.DropIndex(
                name: "IX_Earnings_RuleId",
                table: "Earnings");

            migrationBuilder.DropColumn(
                name: "EarnerId",
                table: "Earnings");

            migrationBuilder.AlterColumn<int>(
                name: "RuleId",
                table: "Earnings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
