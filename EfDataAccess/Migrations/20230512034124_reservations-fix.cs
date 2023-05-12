using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class reservationsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_CreatedByUserId",
                schema: "lib",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_ModifiedByUserId",
                schema: "lib",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_CreatedByUserId",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_ModifiedByUserId",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Users_CreatedByUserId",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Users_ModifiedByUserId",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_CreatedByUserId",
                schema: "lib",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_ModifiedByUserId",
                schema: "lib",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_CreatedByUserId",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_ModifiedByUserId",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CreatedByUserId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ModifiedByUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CreatedByUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ModifiedByUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CreatedByUserId",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ModifiedByUserId",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Rates_CreatedByUserId",
                schema: "lib",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_ModifiedByUserId",
                schema: "lib",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_CreatedByUserId",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_ModifiedByUserId",
                schema: "lib",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Books_CreatedByUserId",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ModifiedByUserId",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_CreatedByUserId",
                schema: "lib",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_ModifiedByUserId",
                schema: "lib",
                table: "Authors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedByUserId",
                table: "Users",
                column: "CreatedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModifiedByUserId",
                table: "Users",
                column: "ModifiedByUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CreatedByUserId",
                schema: "lib",
                table: "Reservations",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ModifiedByUserId",
                schema: "lib",
                table: "Reservations",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CreatedByUserId",
                schema: "lib",
                table: "Rates",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_ModifiedByUserId",
                schema: "lib",
                table: "Rates",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_CreatedByUserId",
                schema: "lib",
                table: "Publishers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_ModifiedByUserId",
                schema: "lib",
                table: "Publishers",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedByUserId",
                schema: "lib",
                table: "Books",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ModifiedByUserId",
                schema: "lib",
                table: "Books",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CreatedByUserId",
                schema: "lib",
                table: "Authors",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ModifiedByUserId",
                schema: "lib",
                table: "Authors",
                column: "ModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_CreatedByUserId",
                schema: "lib",
                table: "Authors",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_ModifiedByUserId",
                schema: "lib",
                table: "Authors",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_CreatedByUserId",
                schema: "lib",
                table: "Books",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_ModifiedByUserId",
                schema: "lib",
                table: "Books",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Users_CreatedByUserId",
                schema: "lib",
                table: "Publishers",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Users_ModifiedByUserId",
                schema: "lib",
                table: "Publishers",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_CreatedByUserId",
                schema: "lib",
                table: "Rates",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_ModifiedByUserId",
                schema: "lib",
                table: "Rates",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_CreatedByUserId",
                schema: "lib",
                table: "Reservations",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_ModifiedByUserId",
                schema: "lib",
                table: "Reservations",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CreatedByUserId",
                table: "Users",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ModifiedByUserId",
                table: "Users",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
