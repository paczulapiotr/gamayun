using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamayun.Infrastucture.Migrations
{
    public partial class EntitiesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presence_PresenceDate_PresenceDateID",
                table: "Presence");

            migrationBuilder.DropForeignKey(
                name: "FK_Presence_Student_StudentID",
                table: "Presence");

            migrationBuilder.DropForeignKey(
                name: "FK_PresenceDate_Sections_SectionID",
                table: "PresenceDate");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Topic_TopicID",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSection_Student_StudentID",
                table: "StudentSection");

            migrationBuilder.DropForeignKey(
                name: "FK_Topic_Teacher_TeacherID",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PresenceDate",
                table: "PresenceDate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Presence",
                table: "Presence");

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "Topics");

            migrationBuilder.RenameTable(
                name: "Teacher",
                newName: "Teachers");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "PresenceDate",
                newName: "PresenceDates");

            migrationBuilder.RenameTable(
                name: "Presence",
                newName: "Presences");

            migrationBuilder.RenameIndex(
                name: "IX_Topic_TeacherID",
                table: "Topics",
                newName: "IX_Topics_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_PresenceDate_SectionID",
                table: "PresenceDates",
                newName: "IX_PresenceDates_SectionID");

            migrationBuilder.RenameIndex(
                name: "IX_Presence_StudentID",
                table: "Presences",
                newName: "IX_Presences_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Presence_PresenceDateID",
                table: "Presences",
                newName: "IX_Presences_PresenceDateID");

            migrationBuilder.AddColumn<string>(
                name: "AppUserID",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserID",
                table: "Students",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionID",
                table: "PresenceDates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PresenceDates",
                table: "PresenceDates",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Presences",
                table: "Presences",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AppUserID",
                table: "Teachers",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AppUserID",
                table: "Students",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_PresenceDates_Sections_SectionID",
                table: "PresenceDates",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_PresenceDates_PresenceDateID",
                table: "Presences",
                column: "PresenceDateID",
                principalTable: "PresenceDates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_Students_StudentID",
                table: "Presences",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Topics_TopicID",
                table: "Sections",
                column: "TopicID",
                principalTable: "Topics",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_AppUserID",
                table: "Students",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSection_Students_StudentID",
                table: "StudentSection",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_AspNetUsers_AppUserID",
                table: "Teachers",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_PresenceDates_Sections_SectionID",
                table: "PresenceDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Presences_PresenceDates_PresenceDateID",
                table: "Presences");

            migrationBuilder.DropForeignKey(
                name: "FK_Presences_Students_StudentID",
                table: "Presences");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Topics_TopicID",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_AppUserID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSection_Students_StudentID",
                table: "StudentSection");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_AspNetUsers_AppUserID",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Teachers_TeacherID",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_AppUserID",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AppUserID",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Presences",
                table: "Presences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PresenceDates",
                table: "PresenceDates");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "Topic");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Teacher");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Presences",
                newName: "Presence");

            migrationBuilder.RenameTable(
                name: "PresenceDates",
                newName: "PresenceDate");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_TeacherID",
                table: "Topic",
                newName: "IX_Topic_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_Presences_StudentID",
                table: "Presence",
                newName: "IX_Presence_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Presences_PresenceDateID",
                table: "Presence",
                newName: "IX_Presence_PresenceDateID");

            migrationBuilder.RenameIndex(
                name: "IX_PresenceDates_SectionID",
                table: "PresenceDate",
                newName: "IX_PresenceDate_SectionID");

            migrationBuilder.AlterColumn<int>(
                name: "SectionID",
                table: "PresenceDate",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Presence",
                table: "Presence",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PresenceDate",
                table: "PresenceDate",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Presence_PresenceDate_PresenceDateID",
                table: "Presence",
                column: "PresenceDateID",
                principalTable: "PresenceDate",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Presence_Student_StudentID",
                table: "Presence",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PresenceDate_Sections_SectionID",
                table: "PresenceDate",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Topic_TopicID",
                table: "Sections",
                column: "TopicID",
                principalTable: "Topic",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSection_Student_StudentID",
                table: "StudentSection",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_Teacher_TeacherID",
                table: "Topic",
                column: "TeacherID",
                principalTable: "Teacher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
