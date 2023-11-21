using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedSomeSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "lib",
                table: "Category",
                columns: new[] { "Id", "CreatedByUserId", "Description", "ModifiedByUserId", "Name" },
                values: new object[] { 2, 1, "Trileri", 1, "Trileri" });

            migrationBuilder.InsertData(
                schema: "lib",
                table: "Department",
                columns: new[] { "Id", "Budget", "CreatedByUserId", "Description", "Location", "ModifiedByUserId", "Name", "ParentDepartmentId" },
                values: new object[] { 2, 100000m, 1, "Departman 21-og veka svetske književnosti", null, 1, "21st century", null });

            migrationBuilder.InsertData(
                schema: "lib",
                table: "Publisher",
                columns: new[] { "Id", "CreatedByUserId", "Description", "ModifiedByUserId", "Name" },
                values: new object[] { 2, 1, "Laguna knjižare", 1, "Laguna" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Department",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 9, 4, 2, 53, 10, 671, DateTimeKind.Local).AddTicks(5656));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 9, 4, 2, 53, 10, 671, DateTimeKind.Local).AddTicks(5728));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 9, 4, 2, 53, 10, 671, DateTimeKind.Local).AddTicks(5733));
        }
    }
}
