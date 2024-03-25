using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedinitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "lib",
                table: "Author",
                columns: new[] { "Id", "CreatedByUserId", "ModifiedByUserId", "Name" },
                values: new object[,]
                {
                    { 1, 1, 1, "Ivo Andrić" },
                    { 2, 1, 1, "Fridrih Niče" }
                });

            migrationBuilder.InsertData(
                schema: "lib",
                table: "Category",
                columns: new[] { "Id", "CreatedByUserId", "Description", "ModifiedByUserId", "Name" },
                values: new object[] { 1, 1, "Klasici svetske književnosti", 1, "Klasici" });

            migrationBuilder.InsertData(
                schema: "lib",
                table: "Department",
                columns: new[] { "Id", "Budget", "CreatedByUserId", "Description", "Location", "ModifiedByUserId", "Name", "ParentDepartmentId" },
                values: new object[] { 1, 100000m, 1, "Departman klasika svetske književnosti", null, 1, "Klasici", null });

            migrationBuilder.InsertData(
                schema: "lib",
                table: "Publisher",
                columns: new[] { "Id", "CreatedByUserId", "Description", "ModifiedByUserId", "Name" },
                values: new object[] { 1, 1, "Delfi knjižare", 1, "Delfi" });

            migrationBuilder.InsertData(
                schema: "lib",
                table: "Rate",
                columns: new[] { "Id", "ApplyAfterDays", "CreatedByUserId", "ModifiedByUserId", "RateFee" },
                values: new object[] { 1, 21, 1, 1, 50m });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 26, 34, 542, DateTimeKind.Local).AddTicks(5945));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 26, 34, 542, DateTimeKind.Local).AddTicks(6022));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 26, 34, 542, DateTimeKind.Local).AddTicks(6027));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Author",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Author",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Department",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Rate",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 24, 48, 34, DateTimeKind.Local).AddTicks(3716));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 24, 48, 34, DateTimeKind.Local).AddTicks(3785));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 24, 48, 34, DateTimeKind.Local).AddTicks(3790));
        }
    }
}
