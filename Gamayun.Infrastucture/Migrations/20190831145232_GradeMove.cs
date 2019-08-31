using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamayun.Infrastucture.Migrations
{
    public partial class GradeMove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentSections");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Sections",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Sections");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "StudentSections",
                nullable: true);
        }
    }
}
