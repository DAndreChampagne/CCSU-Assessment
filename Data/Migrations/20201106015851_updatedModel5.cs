using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class updatedModel5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Faculty_FacultyId1",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Faculty_FacultyId1",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_FacultyId1",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_CourseSections_FacultyId1",
                table: "CourseSections");

            migrationBuilder.DropColumn(
                name: "FacultyId1",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "FacultyId1",
                table: "CourseSections");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Scores",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "CourseSections",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_FacultyId",
                table: "Scores",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_FacultyId",
                table: "CourseSections",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Faculty_FacultyId",
                table: "CourseSections",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Faculty_FacultyId",
                table: "Scores",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Faculty_FacultyId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Faculty_FacultyId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_FacultyId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_CourseSections_FacultyId",
                table: "CourseSections");

            migrationBuilder.AlterColumn<string>(
                name: "FacultyId",
                table: "Scores",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "FacultyId1",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FacultyId",
                table: "CourseSections",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "FacultyId1",
                table: "CourseSections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_FacultyId1",
                table: "Scores",
                column: "FacultyId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_FacultyId1",
                table: "CourseSections",
                column: "FacultyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Faculty_FacultyId1",
                table: "CourseSections",
                column: "FacultyId1",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Faculty_FacultyId1",
                table: "Scores",
                column: "FacultyId1",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
