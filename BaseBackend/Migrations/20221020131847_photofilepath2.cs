using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseBackend.Migrations
{
    public partial class photofilepath2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoFilePath",
                table: "Employees",
                newName: "PhotoFileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoFileName",
                table: "Employees",
                newName: "PhotoFilePath");
        }
    }
}
