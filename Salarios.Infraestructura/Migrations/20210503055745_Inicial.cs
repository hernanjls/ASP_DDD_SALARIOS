using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Salarios.Infraestructura.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblDivisiones",
                columns: table => new
                {
                    DivisionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDivisiones", x => x.DivisionId);
                });

            migrationBuilder.CreateTable(
                name: "TblEmployee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "varchar(150)", nullable: false),
                    EmployeeSurName = table.Column<string>(type: "varchar(150)", nullable: false),
                    DivisionId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    Office = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    EmployeeCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "varchar(10)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEmployee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "TblSalarios",
                columns: table => new
                {
                    SalarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionBonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompensationBonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Commission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Contributions = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSalarios", x => x.SalarioId);
                });

            migrationBuilder.InsertData(
                table: "TblDivisiones",
                columns: new[] { "DivisionId", "DivisionName" },
                values: new object[,]
                {
                    { 1, "OPERATION" },
                    { 2, "SALES" },
                    { 3, "CUSTOMER CARE" },
                    { 4, "MARKETING" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblDivisiones");

            migrationBuilder.DropTable(
                name: "TblEmployee");

            migrationBuilder.DropTable(
                name: "TblSalarios");
        }
    }
}
