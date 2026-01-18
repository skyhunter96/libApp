using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedauthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "lib",
                table: "Author",
                columns: new[] { "Id", "CreatedByUserId", "ModifiedByUserId", "Name" },
                values: new object[] { 3, 1, 1, "Laza Kostić" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 24, 20, 15, 14, 985, DateTimeKind.Local).AddTicks(5819));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 24, 20, 15, 14, 985, DateTimeKind.Local).AddTicks(5888));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 24, 20, 15, 14, 985, DateTimeKind.Local).AddTicks(5899));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Author",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 20, 0, 38, 34, 755, DateTimeKind.Local).AddTicks(4808));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 20, 0, 38, 34, 755, DateTimeKind.Local).AddTicks(4880));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 20, 0, 38, 34, 755, DateTimeKind.Local).AddTicks(4885));
        }
    }
}

