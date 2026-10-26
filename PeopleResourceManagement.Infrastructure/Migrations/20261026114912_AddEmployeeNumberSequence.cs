using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleResourceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeNumberSequence : Migration
    {
        /// <inheritdoc />
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Create a PostgreSQL sequence
            migrationBuilder.CreateSequence<int>(
                name: "EmployeeNumberSeq");

            // 2. Set sequence to the current maximum EmployeeNumber
            migrationBuilder.Sql("""
                                 SELECT setval(
                                     'public."EmployeeNumberSeq"'::regclass,
                                     GREATEST(
                                         (SELECT COALESCE(MAX("EmployeeNumber"), 0)
                                          FROM "Employees"),
                                         1
                                     ),
                                     true
                                 );
                                 """);

            // 3. Set the default value for EmployeeNumber
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeNumber",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('\"EmployeeNumberSeq\"')",
                oldClrType: typeof(int),
                oldType: "integer");
        }


        /// <inheritdoc />
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Remove the default value
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeNumber",
                table: "Employees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValueSql: "nextval('\"EmployeeNumberSeq\"')");

            // 2. Drop the sequence
            migrationBuilder.DropSequence(
                name: "EmployeeNumberSeq");
        }
    }
}
