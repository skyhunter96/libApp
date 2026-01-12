using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class appLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Log",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Log",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogType",
                table: "Log",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Log",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StackTrace",
                table: "Log",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Log",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Log_UserId",
                table: "Log",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Users_UserId",
                table: "Log",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_Users_UserId",
                table: "Log");

            migrationBuilder.DropIndex(
                name: "IX_Log_UserId",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "LogType",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "StackTrace",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Log");
        }
    }
}

