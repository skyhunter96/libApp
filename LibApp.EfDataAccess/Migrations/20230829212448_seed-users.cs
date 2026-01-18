using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Active", "Address", "CardCode", "City", "CreatedByUserId", "Currency", "DateOfBirth", "Email", "FirstName", "ImagePath", "IsCardActive", "IsVerified", "LastLoginDateTime", "LastName", "ModifiedByUserId", "Notes", "Password", "Phone", "RegistrationDateTime", "RoleId", "TotalFee", "Username", "VerificationSentAt", "VerificationToken" },
                values: new object[,]
                {
                    { 1, true, "nema ulice bb", "123-456-789", "Belgrade", 1, null, new DateTime(1996, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "misteryx96@yahoo.com", "Mladen", null, true, true, null, "Karic", 1, null, "giomlly", "0611234567", new DateTime(2023, 8, 29, 23, 24, 48, 34, DateTimeKind.Local).AddTicks(3716), 1, 0m, "giomlly", null, null },
                    { 2, true, "nema ulice bb", "111-456-789", "Belgrade", 1, null, new DateTime(1996, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "mirko@yahoo.com", "Mirko", null, true, true, null, "Cvetkovic", 1, null, "mirko", "0621234567", new DateTime(2023, 8, 29, 23, 24, 48, 34, DateTimeKind.Local).AddTicks(3785), 2, 0m, "mirko", null, null },
                    { 3, true, "nema ulice bb", "222-456-789", "Belgrade", 1, null, new DateTime(1996, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "marko@yahoo.com", "Marko", null, true, true, null, "Nikolic", 1, null, "marko", "0631234567", new DateTime(2023, 8, 29, 23, 24, 48, 34, DateTimeKind.Local).AddTicks(3790), 3, 0m, "marko", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

