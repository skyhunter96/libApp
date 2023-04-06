using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class authors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                schema: "lib",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Departments",
                schema: "lib",
                newName: "Department",
                newSchema: "lib");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                schema: "lib",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Authors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "lib",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                schema: "lib",
                table: "Department",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                schema: "lib",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "lib",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Department",
                schema: "lib",
                newName: "Departments",
                newSchema: "lib");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                schema: "lib",
                table: "Departments",
                column: "Id");
        }
    }
}
