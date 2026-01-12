using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedbooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "lib",
                table: "Book",
                columns: new[] { "Id", "AvailableQuantity", "CategoryId", "Cost", "CreatedByUserId", "DepartmentId", "Description", "Edition", "ImagePath", "IsAvailable", "Isbn", "LanguageId", "ModifiedByUserId", "PublisherId", "Quantity", "ReleaseYear", "ReservedQuantity", "Title" },
                values: new object[,]
                {
                    { 1, 10, 1, null, 1, 1, "Najpoznatije delo Ive Andrića", "Second", null, true, "978-86-6249-252-4", 1, 1, 1, 10, 2021, 0, "Na Drini Ćuprija" },
                    { 2, 15, 1, null, 1, 1, "A dystopian novel by George Orwell", "First", null, true, "978-0-452-28423-4", 2, 1, 1, 15, 1949, 0, "1984" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 30, 7, 456, DateTimeKind.Local).AddTicks(1615));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 30, 7, 456, DateTimeKind.Local).AddTicks(1688));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 30, 7, 456, DateTimeKind.Local).AddTicks(1697));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);

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
    }
}

