using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class datamodel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Sections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Criterion01",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion02",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion03",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion04",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion05",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion06",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion07",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion08",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion09",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion10",
                table: "Rubrics",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false),
                    RubricId = table.Column<int>(nullable: false),
                    ArtifactId = table.Column<int>(nullable: false),
                    Score01 = table.Column<int>(nullable: true),
                    Score02 = table.Column<int>(nullable: true),
                    Score03 = table.Column<int>(nullable: true),
                    Score04 = table.Column<int>(nullable: true),
                    Score05 = table.Column<int>(nullable: true),
                    Score06 = table.Column<int>(nullable: true),
                    Score07 = table.Column<int>(nullable: true),
                    Score08 = table.Column<int>(nullable: true),
                    Score09 = table.Column<int>(nullable: true),
                    Score10 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_Artifacts_ArtifactId",
                        column: x => x.ArtifactId,
                        principalTable: "Artifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Rubrics_RubricId",
                        column: x => x.RubricId,
                        principalTable: "Rubrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_UserId",
                table: "Sections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ArtifactId",
                table: "Scores",
                column: "ArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RubricId",
                table: "Scores",
                column: "RubricId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_SchoolId",
                table: "Scores",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_UserId",
                table: "Scores",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Users_UserId",
                table: "Sections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Users_UserId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Sections_UserId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Criterion01",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Criterion02",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Criterion03",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Criterion04",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Criterion05",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Criterion06",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Criterion07",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Criterion08",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Criterion09",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "Criterion10",
                table: "Rubrics");
        }
    }
}
