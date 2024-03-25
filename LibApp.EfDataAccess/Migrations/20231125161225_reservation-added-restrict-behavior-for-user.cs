using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class reservationaddedrestrictbehaviorforuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_ReservedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 25, 17, 12, 24, 965, DateTimeKind.Local).AddTicks(6263));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 25, 17, 12, 24, 965, DateTimeKind.Local).AddTicks(6328));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationDateTime",
                value: new DateTime(2023, 11, 25, 17, 12, 24, 965, DateTimeKind.Local).AddTicks(6333));

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_ReservedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "ReservedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_ReservedByUserId",
                schema: "lib",
                table: "Reservation");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_ReservedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "ReservedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
