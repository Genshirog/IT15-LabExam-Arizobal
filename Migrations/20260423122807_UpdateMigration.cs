using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabExam1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NetPay",
                table: "payrolls",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "GrossPay",
                table: "payrolls",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<decimal>(
                name: "Deduction",
                table: "payrolls",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_payrolls_EmployeeId",
                table: "payrolls",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_payrolls_employees_EmployeeId",
                table: "payrolls",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payrolls_employees_EmployeeId",
                table: "payrolls");

            migrationBuilder.DropIndex(
                name: "IX_payrolls_EmployeeId",
                table: "payrolls");

            migrationBuilder.AlterColumn<int>(
                name: "NetPay",
                table: "payrolls",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<double>(
                name: "GrossPay",
                table: "payrolls",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<int>(
                name: "Deduction",
                table: "payrolls",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }
    }
}
