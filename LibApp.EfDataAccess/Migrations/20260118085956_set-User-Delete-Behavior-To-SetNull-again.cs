using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class setUserDeleteBehaviorToSetNullagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_User_CreatedByUserId",
                schema: "lib",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_User_ModifiedByUserId",
                schema: "lib",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_User_CreatedByUserId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_User_ModifiedByUserId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_CreatedByUserId",
                schema: "lib",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_ModifiedByUserId",
                schema: "lib",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_CreatedByUserId",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_ModifiedByUserId",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_User_CreatedByUserId",
                schema: "lib",
                table: "Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_User_ModifiedByUserId",
                schema: "lib",
                table: "Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_CreatedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_ModifiedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f4dae5f5-835f-426f-89cd-930ecf14a594");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "40081e48-bd9e-45c4-a7ec-5161f629fb98");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d94e1807-3300-41fd-9ee6-a40a24506b1f");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_User_CreatedByUserId",
                schema: "lib",
                table: "Author",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_User_ModifiedByUserId",
                schema: "lib",
                table: "Author",
                column: "ModifiedByUserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_User_CreatedByUserId",
                schema: "lib",
                table: "Book",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_User_ModifiedByUserId",
                schema: "lib",
                table: "Book",
                column: "ModifiedByUserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_CreatedByUserId",
                schema: "lib",
                table: "Category",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_ModifiedByUserId",
                schema: "lib",
                table: "Category",
                column: "ModifiedByUserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_User_CreatedByUserId",
                schema: "lib",
                table: "Department",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_User_ModifiedByUserId",
                schema: "lib",
                table: "Department",
                column: "ModifiedByUserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Publisher_User_CreatedByUserId",
                schema: "lib",
                table: "Publisher",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Publisher_User_ModifiedByUserId",
                schema: "lib",
                table: "Publisher",
                column: "ModifiedByUserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_CreatedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_ModifiedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "ModifiedByUserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_User_CreatedByUserId",
                schema: "lib",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_User_ModifiedByUserId",
                schema: "lib",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_User_CreatedByUserId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_User_ModifiedByUserId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_CreatedByUserId",
                schema: "lib",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_ModifiedByUserId",
                schema: "lib",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_CreatedByUserId",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_ModifiedByUserId",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_User_CreatedByUserId",
                schema: "lib",
                table: "Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_User_ModifiedByUserId",
                schema: "lib",
                table: "Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_CreatedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_ModifiedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "45784e6f-fe5b-48e2-ae1b-3b5a6b253cbc");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4d569622-bf31-48a4-a40e-9f229bda4c3a");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8859de22-df20-4a95-9beb-0fec514b685f");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_User_CreatedByUserId",
                schema: "lib",
                table: "Author",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_User_ModifiedByUserId",
                schema: "lib",
                table: "Author",
                column: "ModifiedByUserId",
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
                name: "FK_Book_User_ModifiedByUserId",
                schema: "lib",
                table: "Book",
                column: "ModifiedByUserId",
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
                name: "FK_Category_User_ModifiedByUserId",
                schema: "lib",
                table: "Category",
                column: "ModifiedByUserId",
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
                name: "FK_Department_User_ModifiedByUserId",
                schema: "lib",
                table: "Department",
                column: "ModifiedByUserId",
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
                name: "FK_Publisher_User_ModifiedByUserId",
                schema: "lib",
                table: "Publisher",
                column: "ModifiedByUserId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_ModifiedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "ModifiedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
