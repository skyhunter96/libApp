using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class rates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                schema: "lib",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Rates",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Rates",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<decimal>(
                name: "RateFee",
                schema: "lib",
                table: "Rates",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                schema: "lib",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "RateFee",
                schema: "lib",
                table: "Rates");
        }
    }
}
