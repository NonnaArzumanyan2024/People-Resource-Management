using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleResourceManagement.Infrastructure.Migrations;

public partial class ChangeEmployeeNumberToInt : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Remove the old string EmployeeNumber
        migrationBuilder.DropColumn(
            name: "EmployeeNumber",
            table: "Employees");

        // Add a new nullable integer column
        migrationBuilder.AddColumn<int>(
            name: "EmployeeNumber",
            table: "Employees",
            type: "integer",
            nullable: true);

        // Assign Employee Id as EmployeeNumber
        migrationBuilder.Sql("""
                             UPDATE "Employees"
                             SET "EmployeeNumber" = "Id";
                             """);

        // Make EmployeeNumber NOT NULL
        migrationBuilder.AlterColumn<int>(
            name: "EmployeeNumber",
            table: "Employees",
            type: "integer",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer",
            oldNullable: true);

        // Create unique index
        migrationBuilder.CreateIndex(
            name: "IX_Employees_EmployeeNumber",
            table: "Employees",
            column: "EmployeeNumber",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Remove unique index
        migrationBuilder.DropIndex(
            name: "IX_Employees_EmployeeNumber",
            table: "Employees");

        // Convert integer numbers back to text
        migrationBuilder.Sql("""
                             ALTER TABLE "Employees"
                             ALTER COLUMN "EmployeeNumber"
                             TYPE text
                             USING "EmployeeNumber"::text;
                             """);
    }
}