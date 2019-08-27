using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamayun.Infrastucture.Migrations
{
    public partial class SectionsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Teachers_TeacherID",
                table: "Topics");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherID",
                table: "Topics",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sections",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Teachers_TeacherID",
                table: "Topics",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Teachers_TeacherID",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sections");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherID",
                table: "Topics",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Teachers_TeacherID",
                table: "Topics",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
