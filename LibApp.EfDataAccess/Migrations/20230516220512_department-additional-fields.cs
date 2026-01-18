using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class departmentadditionalfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                schema: "lib",
                table: "Department",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "lib",
                table: "Department",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentDepartmentId",
                schema: "lib",
                table: "Department",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Budget",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "ParentDepartmentId",
                schema: "lib",
                table: "Department");
        }
    }
}

