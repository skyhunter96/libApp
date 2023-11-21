using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixedSomeSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "lib",
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "DepartmentId", "PublisherId" },
                values: new object[] { 2, 2, 2 });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "lib",
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "DepartmentId", "PublisherId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 20, 0, 33, 35, 900, DateTimeKind.Local).AddTicks(9400));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 20, 0, 33, 35, 900, DateTimeKind.Local).AddTicks(9464));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 20, 0, 33, 35, 900, DateTimeKind.Local).AddTicks(9468));
        }
    }
}
