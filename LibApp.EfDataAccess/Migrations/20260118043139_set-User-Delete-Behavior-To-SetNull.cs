using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class setUserDeleteBehaviorToSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_User_CreatedByUserId",
                schema: "lib",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_User_CreatedByUserId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_CreatedByUserId",
                schema: "lib",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_CreatedByUserId",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_User_CreatedByUserId",
                schema: "lib",
                table: "Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_CreatedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "edfe142a-1e0e-4377-812f-6f3cb3d255f8");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "564d3527-5e27-4933-89ae-bdf9bae47e62");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7617b590-deb4-4a53-9f91-945682cf5155");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_User_CreatedByUserId",
                schema: "lib",
                table: "Author",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_User_CreatedByUserId",
                schema: "lib",
                table: "Book",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_CreatedByUserId",
                schema: "lib",
                table: "Category",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_User_CreatedByUserId",
                schema: "lib",
                table: "Department",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Publisher_User_CreatedByUserId",
                schema: "lib",
                table: "Publisher",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_CreatedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_User_CreatedByUserId",
                schema: "lib",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_User_CreatedByUserId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_CreatedByUserId",
                schema: "lib",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_CreatedByUserId",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_User_CreatedByUserId",
                schema: "lib",
                table: "Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_CreatedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "1ee3e3bd-719f-4892-b740-38a8d5a8c7bc");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5835dc13-17b3-4bf7-92b6-b4f918235b28");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "56b31b36-5e58-4c23-8410-1a6392be0382");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_User_CreatedByUserId",
                schema: "lib",
                table: "Author",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_User_CreatedByUserId",
                schema: "lib",
                table: "Book",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_CreatedByUserId",
                schema: "lib",
                table: "Category",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_User_CreatedByUserId",
                schema: "lib",
                table: "Department",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Publisher_User_CreatedByUserId",
                schema: "lib",
                table: "Publisher",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_CreatedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
