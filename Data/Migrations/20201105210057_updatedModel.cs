using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class updatedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artifacts_Rubrics_RubricId",
                table: "Artifacts");

            migrationBuilder.DropForeignKey(
                name: "FK_RubricCriteria_Rubrics_RubricId",
                table: "RubricCriteria");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Rubrics_RubricId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_RubricId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_RubricCriteria_RubricId",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "LearningObjective",
                table: "Artifacts");

            migrationBuilder.AddColumn<string>(
                name: "RubricId1",
                table: "Scores",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Rubrics",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "RubricId1",
                table: "RubricCriteria",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RubricId",
                table: "Artifacts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RubricId1",
                table: "Scores",
                column: "RubricId1");

            migrationBuilder.CreateIndex(
                name: "IX_RubricCriteria_RubricId1",
                table: "RubricCriteria",
                column: "RubricId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Artifacts_Rubrics_RubricId",
                table: "Artifacts",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RubricCriteria_Rubrics_RubricId1",
                table: "RubricCriteria",
                column: "RubricId1",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Rubrics_RubricId1",
                table: "Scores",
                column: "RubricId1",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artifacts_Rubrics_RubricId",
                table: "Artifacts");

            migrationBuilder.DropForeignKey(
                name: "FK_RubricCriteria_Rubrics_RubricId1",
                table: "RubricCriteria");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Rubrics_RubricId1",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_RubricId1",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_RubricCriteria_RubricId1",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "RubricId1",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "RubricId1",
                table: "RubricCriteria");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Rubrics",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "Rubrics",
                type: "varchar(2) CHARACTER SET utf8mb4",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Rubrics",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RubricId",
                table: "Artifacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LearningObjective",
                table: "Artifacts",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RubricId",
                table: "Scores",
                column: "RubricId");

            migrationBuilder.CreateIndex(
                name: "IX_RubricCriteria_RubricId",
                table: "RubricCriteria",
                column: "RubricId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artifacts_Rubrics_RubricId",
                table: "Artifacts",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RubricCriteria_Rubrics_RubricId",
                table: "RubricCriteria",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Rubrics_RubricId",
                table: "Scores",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
