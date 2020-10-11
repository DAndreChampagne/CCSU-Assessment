using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CRN = table.Column<int>(maxLength: 10, nullable: false),
                    SessionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rubrics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 2, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Data = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    ArtifactId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rubrics_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artifacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolId = table.Column<int>(nullable: false),
                    RubricId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Term = table.Column<string>(maxLength: 10, nullable: true),
                    StudentId = table.Column<string>(maxLength: 10, nullable: true),
                    LearningObjective = table.Column<string>(maxLength: 100, nullable: true),
                    Level = table.Column<string>(maxLength: 2, nullable: true),
                    CRN = table.Column<string>(maxLength: 10, nullable: true),
                    FilePath = table.Column<string>(maxLength: 256, nullable: true),
                    File = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artifacts_Rubrics_RubricId",
                        column: x => x.RubricId,
                        principalTable: "Rubrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artifacts_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RubricCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RubricId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Desciption4 = table.Column<string>(nullable: true),
                    Desciption3 = table.Column<string>(nullable: true),
                    Desciption2 = table.Column<string>(nullable: true),
                    Desciption1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubricCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RubricCriteria_Rubrics_RubricId",
                        column: x => x.RubricId,
                        principalTable: "Rubrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_RubricId",
                table: "Artifacts",
                column: "RubricId");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_SchoolId",
                table: "Artifacts",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_RubricCriteria_RubricId",
                table: "RubricCriteria",
                column: "RubricId");

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_ArtifactId",
                table: "Rubrics",
                column: "ArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_SchoolId",
                table: "Rubrics",
                column: "SchoolId");

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
                name: "IX_Sections_SessionId",
                table: "Sections",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SchoolId",
                table: "Sessions",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rubrics_Artifacts_ArtifactId",
                table: "Rubrics",
                column: "ArtifactId",
                principalTable: "Artifacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artifacts_Rubrics_RubricId",
                table: "Artifacts");

            migrationBuilder.DropTable(
                name: "RubricCriteria");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Rubrics");

            migrationBuilder.DropTable(
                name: "Artifacts");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
