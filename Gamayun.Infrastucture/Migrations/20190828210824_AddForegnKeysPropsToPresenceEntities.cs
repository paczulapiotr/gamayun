using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamayun.Infrastucture.Migrations
{
    public partial class AddForegnKeysPropsToPresenceEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresenceDates_Sections_SectionID",
                table: "PresenceDates");

            migrationBuilder.AlterColumn<int>(
                name: "SectionID",
                table: "PresenceDates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PresenceDates_Sections_SectionID",
                table: "PresenceDates",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresenceDates_Sections_SectionID",
                table: "PresenceDates");

            migrationBuilder.AlterColumn<int>(
                name: "SectionID",
                table: "PresenceDates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PresenceDates_Sections_SectionID",
                table: "PresenceDates",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
