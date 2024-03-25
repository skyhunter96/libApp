using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class departmentselfRefrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentDepartmentId",
                schema: "lib",
                table: "Department",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentDepartmentId",
                schema: "lib",
                table: "Department",
                column: "ParentDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Department_ParentDepartmentId",
                schema: "lib",
                table: "Department",
                column: "ParentDepartmentId",
                principalSchema: "lib",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Department_ParentDepartmentId",
                schema: "lib",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_ParentDepartmentId",
                schema: "lib",
                table: "Department");

            migrationBuilder.AlterColumn<int>(
                name: "ParentDepartmentId",
                schema: "lib",
                table: "Department",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
