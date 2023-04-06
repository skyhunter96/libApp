using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class books : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                schema: "lib",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                schema: "lib",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "lib",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                schema: "lib",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                schema: "lib",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                schema: "lib",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                schema: "lib",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "lib",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservedQuantity",
                schema: "lib",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Cost",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Edition",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Isbn",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Photo",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "lib",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReservedQuantity",
                schema: "lib",
                table: "Books");
        }
    }
}
