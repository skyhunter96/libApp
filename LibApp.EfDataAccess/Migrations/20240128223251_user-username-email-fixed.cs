using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class userusernameemailfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE [LibApp].[dbo].[User]
                SET UserName = LTRIM(RTRIM(UserName)),
                    Email = LTRIM(RTRIM(Email));
            ");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9aa2a6ef-88e7-46f0-a537-903b0332d3db");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "25a693fe-dd20-44f9-abc9-99ac7d5bac12");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4363e84c-3b36-41bc-9462-426f9ae6f93b");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "13811342-3790-40d4-ac41-2b5766eef859");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b27a83d7-a86f-41f6-8f6b-4cfb84cb37da");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5ba4e4f2-c570-4800-973b-42c475c2c613");
        }
    }
}

