using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class useridremovedusedfromidentityuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "RegistrationDateTime" },
                values: new object[] { "612628e2-ba19-4058-8061-4e8abb062972", new DateTime(2024, 1, 2, 19, 43, 8, 413, DateTimeKind.Local).AddTicks(8688) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "RegistrationDateTime" },
                values: new object[] { "ad38aeb5-0575-4148-b85b-546d08e8ddfc", new DateTime(2024, 1, 2, 19, 43, 8, 413, DateTimeKind.Local).AddTicks(8802) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "RegistrationDateTime" },
                values: new object[] { "a18a226e-d28d-4c9d-99ae-097b877b624c", new DateTime(2024, 1, 2, 19, 43, 8, 413, DateTimeKind.Local).AddTicks(8817) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "RegistrationDateTime" },
                values: new object[] { "b32988dc-d5aa-4b0a-86d2-fad1563ab723", new DateTime(2024, 1, 1, 21, 1, 2, 603, DateTimeKind.Local).AddTicks(9956) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "RegistrationDateTime" },
                values: new object[] { "119e29e3-cf7b-4300-92ea-10c316b2545d", new DateTime(2024, 1, 1, 21, 1, 2, 604, DateTimeKind.Local).AddTicks(26) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "RegistrationDateTime" },
                values: new object[] { "358a49c1-a12a-4087-be35-59964c3ce15b", new DateTime(2024, 1, 1, 21, 1, 2, 604, DateTimeKind.Local).AddTicks(58) });
        }
    }
}

