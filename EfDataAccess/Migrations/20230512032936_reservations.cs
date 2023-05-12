using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class reservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualReturnDate",
                schema: "lib",
                table: "Reservations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                schema: "lib",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                schema: "lib",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "LateFee",
                schema: "lib",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "LoanDate",
                schema: "lib",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<int>(
                name: "ReservedByUserId",
                schema: "lib",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Reservations_ReservedByUserId",
                schema: "lib",
                table: "Reservations",
                column: "ReservedByUserId");

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
                name: "FK_Reservations_Users_ReservedByUserId",
                schema: "lib",
                table: "Reservations",
                column: "ReservedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Reservations_Users_ReservedByUserId",
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
                name: "IX_Reservations_ReservedByUserId",
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

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActualReturnDate",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DueDate",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LateFee",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LoanDate",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservedByUserId",
                schema: "lib",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
