using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedbookauthorsrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "lib",
                table: "BookAuthor",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 31, 29, 685, DateTimeKind.Local).AddTicks(4700));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 31, 29, 685, DateTimeKind.Local).AddTicks(4768));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 8, 29, 23, 31, 29, 685, DateTimeKind.Local).AddTicks(4772));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "lib",
                table: "BookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "BookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 2 });

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
    }
}
