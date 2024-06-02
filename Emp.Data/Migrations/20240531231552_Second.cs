using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentDepId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentDepId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentDepId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepId",
                table: "Employees",
                column: "DepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepId",
                table: "Employees",
                column: "DepId",
                principalTable: "Departments",
                principalColumn: "DepId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentDepId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentDepId",
                table: "Employees",
                column: "DepartmentDepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentDepId",
                table: "Employees",
                column: "DepartmentDepId",
                principalTable: "Departments",
                principalColumn: "DepId");
        }
    }
}
