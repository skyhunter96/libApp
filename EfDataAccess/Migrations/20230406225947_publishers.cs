using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class publishers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                schema: "lib",
                table: "Publishers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Publishers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "lib",
                table: "Publishers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Publishers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Publishers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "lib",
                table: "Publishers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "lib",
                table: "Publishers");
        }
    }
}
