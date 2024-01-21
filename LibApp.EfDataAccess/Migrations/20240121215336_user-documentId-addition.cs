using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class userdocumentIdaddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "char(50)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(50)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                table: "User",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DocumentId" },
                values: new object[] { "f1d804eb-3e76-40b9-9241-085ce8d3321b", "0702995760010" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DocumentId" },
                values: new object[] { "d0c66401-e140-4a0f-858c-48fe9dd1b990", "0702995760011" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "DocumentId" },
                values: new object[] { "0ffc6058-8349-44e8-9acf-cd9e68d4e2be", "0702995760012" });

            migrationBuilder.CreateIndex(
                name: "IX_User_DocumentId",
                table: "User",
                column: "DocumentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_DocumentId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "char(50)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(50)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "197d25ee-75ca-4036-aeef-0758f3511a66");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b3571e2b-4837-429f-a9cd-39ad2ff69b90");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3eef5ed2-9b24-4357-90c3-f3591af6644f");
        }
    }
}
