using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class identityuserandroleonetooneconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "RegistrationDateTime" },
                values: new object[] { "773fb59b-5504-4b7b-87a9-a30bcb42bfbc", new DateTime(2024, 1, 1, 20, 47, 57, 747, DateTimeKind.Local).AddTicks(779) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "RegistrationDateTime" },
                values: new object[] { "a0eb6c72-fd6d-4d61-8117-cf1e70ca73e7", new DateTime(2024, 1, 1, 20, 47, 57, 747, DateTimeKind.Local).AddTicks(874) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "RegistrationDateTime" },
                values: new object[] { "37ef5437-6a5b-4e17-b410-620cbc65c19c", new DateTime(2024, 1, 1, 20, 47, 57, 747, DateTimeKind.Local).AddTicks(882) });
        }
    }
}
