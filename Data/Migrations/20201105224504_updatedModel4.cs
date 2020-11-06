using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class updatedModel4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rubrics_Artifacts_ArtifactId",
                table: "Rubrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Rubrics_RubricId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_RubricId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Rubrics_ArtifactId",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "RubricId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score01",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score02",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score03",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score04",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score05",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score06",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score07",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score08",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score09",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score10",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ArtifactId",
                table: "Rubrics");

            migrationBuilder.AddColumn<string>(
                name: "FacultyId",
                table: "Scores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId1",
                table: "Scores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RubricCriteriaId",
                table: "Scores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RubricCriteriaId1",
                table: "Scores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoreValue",
                table: "Scores",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Artifacts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10) CHARACTER SET utf8mb4",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CRN",
                table: "Artifacts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10) CHARACTER SET utf8mb4",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RubricId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculty_Rubrics_RubricId",
                        column: x => x.RubricId,
                        principalTable: "Rubrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseSections",
                columns: table => new
                {
                    CRN = table.Column<int>(nullable: false),
                    FacultyId = table.Column<string>(nullable: true),
                    FacultyId1 = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSections", x => x.CRN);
                    table.ForeignKey(
                        name: "FK_CourseSections_Faculty_FacultyId1",
                        column: x => x.FacultyId1,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_FacultyId1",
                table: "Scores",
                column: "FacultyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RubricCriteriaId1",
                table: "Scores",
                column: "RubricCriteriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_CRN",
                table: "Artifacts",
                column: "CRN");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_FacultyId",
                table: "Artifacts",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_FacultyId1",
                table: "CourseSections",
                column: "FacultyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_RubricId",
                table: "Faculty",
                column: "RubricId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artifacts_CourseSections_CRN",
                table: "Artifacts",
                column: "CRN",
                principalTable: "CourseSections",
                principalColumn: "CRN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Artifacts_Faculty_FacultyId",
                table: "Artifacts",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Faculty_FacultyId1",
                table: "Scores",
                column: "FacultyId1",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_RubricCriteria_RubricCriteriaId1",
                table: "Scores",
                column: "RubricCriteriaId1",
                principalTable: "RubricCriteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artifacts_CourseSections_CRN",
                table: "Artifacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Artifacts_Faculty_FacultyId",
                table: "Artifacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Faculty_FacultyId1",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_RubricCriteria_RubricCriteriaId1",
                table: "Scores");

            migrationBuilder.DropTable(
                name: "CourseSections");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropIndex(
                name: "IX_Scores_FacultyId1",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_RubricCriteriaId1",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Artifacts_CRN",
                table: "Artifacts");

            migrationBuilder.DropIndex(
                name: "IX_Artifacts_FacultyId",
                table: "Artifacts");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "FacultyId1",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "RubricCriteriaId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "RubricCriteriaId1",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ScoreValue",
                table: "Scores");

            migrationBuilder.AddColumn<string>(
                name: "RubricId",
                table: "Scores",
                type: "varchar(2) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score01",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score02",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score03",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score04",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score05",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score06",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score07",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score08",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score09",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score10",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArtifactId",
                table: "Rubrics",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FacultyId",
                table: "Artifacts",
                type: "varchar(10) CHARACTER SET utf8mb4",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CRN",
                table: "Artifacts",
                type: "varchar(10) CHARACTER SET utf8mb4",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RubricId",
                table: "Scores",
                column: "RubricId");

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_ArtifactId",
                table: "Rubrics",
                column: "ArtifactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rubrics_Artifacts_ArtifactId",
                table: "Rubrics",
                column: "ArtifactId",
                principalTable: "Artifacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Rubrics_RubricId",
                table: "Scores",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
