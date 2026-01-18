using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class hopefullyfinalfixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VerificationToken",
                table: "User",
                type: "char(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "char(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "char(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "User",
                type: "char(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplyAfterDays",
                schema: "lib",
                table: "Rate",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                schema: "lib",
                table: "Book",
                type: "char(17)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(50)");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                schema: "lib",
                table: "Book",
                type: "char(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyAfterDays",
                schema: "lib",
                table: "Rate");

            migrationBuilder.AlterColumn<string>(
                name: "VerificationToken",
                table: "User",
                type: "char(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(100)");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "User",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                schema: "lib",
                table: "Book",
                type: "char(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(17)");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                schema: "lib",
                table: "Book",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(255)");
        }
    }
}

