using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamayun.Infrastucture.Migrations
{
    public partial class AddSemesterToSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemesterID",
                table: "Sections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SemesterID",
                table: "Sections",
                column: "SemesterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Semesters_SemesterID",
                table: "Sections",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Semesters_SemesterID",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_SemesterID",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "SemesterID",
                table: "Sections");
        }
    }
}
