using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.EfDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class parentdepartmentidremoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Department_ParentDepartmentId",
                schema: "lib",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "ParentDepartmentId",
                schema: "lib",
                table: "Department",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_ParentDepartmentId",
                schema: "lib",
                table: "Department",
                newName: "IX_Department_DepartmentId");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "22867a65-521a-4bb1-8e46-e86c1291661c");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "14b73946-9f19-4e75-b94d-76401e31b69c");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6b116070-cf26-4b2a-9ece-5f8af07b5b62");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Department_DepartmentId",
                schema: "lib",
                table: "Department",
                column: "DepartmentId",
                principalSchema: "lib",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Department_DepartmentId",
                schema: "lib",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                schema: "lib",
                table: "Department",
                newName: "ParentDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_DepartmentId",
                schema: "lib",
                table: "Department",
                newName: "IX_Department_ParentDepartmentId");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c1caab1f-1f0a-4549-894c-b952c2155bcc");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "76e23aee-c4f1-4c06-9289-059b22cca480");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5fe67de0-49a0-4410-aa44-8b8c47e9aee6");

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
    }
}

