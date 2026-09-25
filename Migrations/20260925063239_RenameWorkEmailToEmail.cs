using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People_Specification.Api.Migrations;

public partial class RenameWorkEmailToEmail : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "WorkEmail",
            table: "Employees",
            newName: "Email");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Email",
            table: "Employees",
            newName: "WorkEmail");
    }
}
