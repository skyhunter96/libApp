using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class reservationreservedByUseroptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReservedByUserId",
                schema: "lib",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f0579826-207f-4688-b5e4-8670c45c882d");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "354209f6-804a-4ed8-a4f5-c0271215ff84");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5ff7e3b9-7ac5-44a2-867d-688f53224705");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReservedByUserId",
                schema: "lib",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3661a4ee-91a5-4f51-9492-ca10d70ea52f");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1fdb9926-a4c4-429a-85e1-ef5445dbc7cc");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ac40da92-0dc1-4402-8a48-f82865889375");
        }
    }
}

