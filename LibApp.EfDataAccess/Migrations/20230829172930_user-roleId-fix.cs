using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class userroleIdfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "User",
                type: "char(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(3)");

            migrationBuilder.InsertData(
                schema: "lib",
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Serbian" },
                    { 2, "English" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedByUserId",
                table: "User",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ModifiedByUserId",
                table: "User",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CreatedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ModifiedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_CreatedByUserId",
                schema: "lib",
                table: "Rate",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_ModifiedByUserId",
                schema: "lib",
                table: "Rate",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_CreatedByUserId",
                schema: "lib",
                table: "Publisher",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_ModifiedByUserId",
                schema: "lib",
                table: "Publisher",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CreatedByUserId",
                schema: "lib",
                table: "Department",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ModifiedByUserId",
                schema: "lib",
                table: "Department",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatedByUserId",
                schema: "lib",
                table: "Category",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ModifiedByUserId",
                schema: "lib",
                table: "Category",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CreatedByUserId",
                schema: "lib",
                table: "Book",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_ModifiedByUserId",
                schema: "lib",
                table: "Book",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_CreatedByUserId",
                schema: "lib",
                table: "Author",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_ModifiedByUserId",
                schema: "lib",
                table: "Author",
                column: "ModifiedByUserId");

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
                name: "FK_Rate_User_CreatedByUserId",
                schema: "lib",
                table: "Rate",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_User_ModifiedByUserId",
                schema: "lib",
                table: "Rate",
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

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatedByUserId",
                table: "User",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_ModifiedByUserId",
                table: "User",
                column: "ModifiedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
                name: "FK_Rate_User_CreatedByUserId",
                schema: "lib",
                table: "Rate");

            migrationBuilder.DropForeignKey(
                name: "FK_Rate_User_ModifiedByUserId",
                schema: "lib",
                table: "Rate");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_CreatedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_ModifiedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatedByUserId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_ModifiedByUserId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CreatedByUserId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ModifiedByUserId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CreatedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ModifiedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Rate_CreatedByUserId",
                schema: "lib",
                table: "Rate");

            migrationBuilder.DropIndex(
                name: "IX_Rate_ModifiedByUserId",
                schema: "lib",
                table: "Rate");

            migrationBuilder.DropIndex(
                name: "IX_Publisher_CreatedByUserId",
                schema: "lib",
                table: "Publisher");

            migrationBuilder.DropIndex(
                name: "IX_Publisher_ModifiedByUserId",
                schema: "lib",
                table: "Publisher");

            migrationBuilder.DropIndex(
                name: "IX_Department_CreatedByUserId",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_ModifiedByUserId",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Category_CreatedByUserId",
                schema: "lib",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ModifiedByUserId",
                schema: "lib",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Book_CreatedByUserId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_ModifiedByUserId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Author_CreatedByUserId",
                schema: "lib",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_ModifiedByUserId",
                schema: "lib",
                table: "Author");

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "User",
                type: "char(3)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
