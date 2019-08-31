using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamayun.Infrastucture.Migrations
{
    public partial class UpdateGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSection_Sections_SectionID",
                table: "StudentSection");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSection_Students_StudentID",
                table: "StudentSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSection",
                table: "StudentSection");

            migrationBuilder.RenameTable(
                name: "StudentSection",
                newName: "StudentSections");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSection_SectionID",
                table: "StudentSections",
                newName: "IX_StudentSections_SectionID");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "StudentSections",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSections",
                table: "StudentSections",
                columns: new[] { "StudentID", "SectionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSections_Sections_SectionID",
                table: "StudentSections",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSections_Students_StudentID",
                table: "StudentSections",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSections_Sections_SectionID",
                table: "StudentSections");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSections_Students_StudentID",
                table: "StudentSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSections",
                table: "StudentSections");

            migrationBuilder.RenameTable(
                name: "StudentSections",
                newName: "StudentSection");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSections_SectionID",
                table: "StudentSection",
                newName: "IX_StudentSection_SectionID");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "StudentSection",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSection",
                table: "StudentSection",
                columns: new[] { "StudentID", "SectionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSection_Sections_SectionID",
                table: "StudentSection",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSection_Students_StudentID",
                table: "StudentSection",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
