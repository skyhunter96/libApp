using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class baseentitygenerictoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                schema: "lib",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Books_BookId",
                schema: "lib",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookReservation_Books_BookId",
                schema: "lib",
                table: "BookReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_BookReservation_Reservations_ReservationId",
                schema: "lib",
                table: "BookReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Department_DepartmentId",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Languages_LanguageId",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_ReservedByUserId",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rates",
                schema: "lib",
                table: "Rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                schema: "lib",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookReservation",
                schema: "lib",
                table: "BookReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                schema: "lib",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "lib",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Languages",
                schema: "lib",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Reservations",
                schema: "lib",
                newName: "Reservation",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Rates",
                schema: "lib",
                newName: "Rate",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Publishers",
                schema: "lib",
                newName: "Publisher",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "lib",
                newName: "Category",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Books",
                schema: "lib",
                newName: "Book",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "BookReservation",
                schema: "lib",
                newName: "BookReservations");

            migrationBuilder.RenameTable(
                name: "Authors",
                schema: "lib",
                newName: "Author",
                newSchema: "lib");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ReservedByUserId",
                schema: "lib",
                table: "Reservation",
                newName: "IX_Reservation_ReservedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_PublisherId",
                schema: "lib",
                table: "Book",
                newName: "IX_Book_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_LanguageId",
                schema: "lib",
                table: "Book",
                newName: "IX_Book_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_DepartmentId",
                schema: "lib",
                table: "Book",
                newName: "IX_Book_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryId",
                schema: "lib",
                table: "Book",
                newName: "IX_Book_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BookReservation_BookId",
                table: "BookReservations",
                newName: "IX_BookReservations_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                schema: "lib",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rate",
                schema: "lib",
                table: "Rate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                schema: "lib",
                table: "Publisher",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                schema: "lib",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                schema: "lib",
                table: "Book",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookReservations",
                table: "BookReservations",
                columns: new[] { "ReservationId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                schema: "lib",
                table: "Author",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category_CategoryId",
                schema: "lib",
                table: "Book",
                column: "CategoryId",
                principalSchema: "lib",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Department_DepartmentId",
                schema: "lib",
                table: "Book",
                column: "DepartmentId",
                principalSchema: "lib",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Languages_LanguageId",
                schema: "lib",
                table: "Book",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                schema: "lib",
                table: "Book",
                column: "PublisherId",
                principalSchema: "lib",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Author_AuthorId",
                schema: "lib",
                table: "BookAuthor",
                column: "AuthorId",
                principalSchema: "lib",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Book_BookId",
                schema: "lib",
                table: "BookAuthor",
                column: "BookId",
                principalSchema: "lib",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservations_Book_BookId",
                table: "BookReservations",
                column: "BookId",
                principalSchema: "lib",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservations_Reservation_ReservationId",
                table: "BookReservations",
                column: "ReservationId",
                principalSchema: "lib",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_User_UserId",
                table: "Logs",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_ReservedByUserId",
                schema: "lib",
                table: "Reservation",
                column: "ReservedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category_CategoryId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Department_DepartmentId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Languages_LanguageId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Author_AuthorId",
                schema: "lib",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Book_BookId",
                schema: "lib",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookReservations_Book_BookId",
                table: "BookReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_BookReservations_Reservation_ReservationId",
                table: "BookReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_User_UserId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_ReservedByUserId",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_RoleId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                schema: "lib",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rate",
                schema: "lib",
                table: "Rate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                schema: "lib",
                table: "Publisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                schema: "lib",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookReservations",
                table: "BookReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                schema: "lib",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                schema: "lib",
                table: "Author");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Roles",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Languages",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Reservation",
                schema: "lib",
                newName: "Reservations",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Rate",
                schema: "lib",
                newName: "Rates",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Publisher",
                schema: "lib",
                newName: "Publishers",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Category",
                schema: "lib",
                newName: "Categories",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "BookReservations",
                newName: "BookReservation",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Book",
                schema: "lib",
                newName: "Books",
                newSchema: "lib");

            migrationBuilder.RenameTable(
                name: "Author",
                schema: "lib",
                newName: "Authors",
                newSchema: "lib");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_ReservedByUserId",
                schema: "lib",
                table: "Reservations",
                newName: "IX_Reservations_ReservedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookReservations_BookId",
                schema: "lib",
                table: "BookReservation",
                newName: "IX_BookReservation_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_PublisherId",
                schema: "lib",
                table: "Books",
                newName: "IX_Books_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_LanguageId",
                schema: "lib",
                table: "Books",
                newName: "IX_Books_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_DepartmentId",
                schema: "lib",
                table: "Books",
                newName: "IX_Books_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_CategoryId",
                schema: "lib",
                table: "Books",
                newName: "IX_Books_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                schema: "lib",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rates",
                schema: "lib",
                table: "Rates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                schema: "lib",
                table: "Publishers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                schema: "lib",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookReservation",
                schema: "lib",
                table: "BookReservation",
                columns: new[] { "ReservationId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                schema: "lib",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                schema: "lib",
                table: "Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                schema: "lib",
                table: "BookAuthor",
                column: "AuthorId",
                principalSchema: "lib",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Books_BookId",
                schema: "lib",
                table: "BookAuthor",
                column: "BookId",
                principalSchema: "lib",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservation_Books_BookId",
                schema: "lib",
                table: "BookReservation",
                column: "BookId",
                principalSchema: "lib",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservation_Reservations_ReservationId",
                schema: "lib",
                table: "BookReservation",
                column: "ReservationId",
                principalSchema: "lib",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                schema: "lib",
                table: "Books",
                column: "CategoryId",
                principalSchema: "lib",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Department_DepartmentId",
                schema: "lib",
                table: "Books",
                column: "DepartmentId",
                principalSchema: "lib",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Languages_LanguageId",
                schema: "lib",
                table: "Books",
                column: "LanguageId",
                principalSchema: "lib",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                schema: "lib",
                table: "Books",
                column: "PublisherId",
                principalSchema: "lib",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_ReservedByUserId",
                schema: "lib",
                table: "Reservations",
                column: "ReservedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalSchema: "lib",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

