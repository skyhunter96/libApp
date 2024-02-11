using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seeddatamore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "lib",
                table: "Category",
                columns: new[] { "Id", "CreatedByUserId", "Description", "ModifiedByUserId", "Name" },
                values: new object[,]
                {
                    { 3, 1, "Filozofija", 1, "Filozofija" },
                    { 4, 1, "Istorija", 1, "Istorija" }
                });

            migrationBuilder.InsertData(
                schema: "lib",
                table: "Department",
                columns: new[] { "Id", "Budget", "CreatedByUserId", "Description", "Location", "ModifiedByUserId", "Name", "ParentDepartmentId" },
                values: new object[,]
                {
                    { 3, 100000m, 1, "Departman 20-og veka svetske književnosti", null, 1, "20th century", null },
                    { 4, 100000m, 1, "Departman 20-og veka svetske književnosti", null, 1, "19th century", null },
                    { 5, 100000m, 1, "Departman filozofije", null, 1, "Filozofija", null }
                });

            migrationBuilder.InsertData(
                schema: "lib",
                table: "Publisher",
                columns: new[] { "Id", "CreatedByUserId", "Description", "ModifiedByUserId", "Name" },
                values: new object[] { 3, 1, "Vulkan knjižare", 1, "Vulkan" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b3a8d274-83e5-46d9-8b53-62b76f1579b3");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "04ba419d-0889-45b8-ba0c-5ddb2678b34a");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "18eadbde-1498-411a-b943-56404d3e873c");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Department",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Department",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Department",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "lib",
                table: "Publisher",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d7089a39-670e-4413-9a99-8bf4acf7c7d2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "089d44fa-7015-426b-9dfe-ce7a8db7d644");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "fe5dad21-c8ed-4861-b3e2-e86077fe8044");
        }
    }
}
