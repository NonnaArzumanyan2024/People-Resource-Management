using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleResourceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeUnitNameCaseInsensitive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Units",
                type: "citext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Units",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "citext");
        }
    }
}
