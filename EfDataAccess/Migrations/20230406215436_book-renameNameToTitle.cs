using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bookrenameNameToTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "lib",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                schema: "lib",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "lib",
                table: "Books",
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
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "lib",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "lib",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
