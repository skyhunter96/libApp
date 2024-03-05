using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bookimgpathtypechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                schema: "lib",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "262d4e27-6730-47ab-809f-62458a107ebb");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "61ee901a-0b4d-464d-b2f4-0a1a4f5a05b9");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8f738623-caea-48f7-abdc-7f98726ce6c5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                schema: "lib",
                table: "Book",
                type: "char(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "1e1fb170-8b90-49b2-9559-f0ea9f8fc356");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6bb536e5-b36f-4a7a-962e-a45e9072eec1");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8055f74b-3e4c-4e5f-99ea-728c8bf2e8d5");
        }
    }
}
