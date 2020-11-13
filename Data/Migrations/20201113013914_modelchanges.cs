using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class modelchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desciption0",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "Desciption1",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "Desciption2",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "Desciption3",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "Desciption4",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Faculty");

            migrationBuilder.AddColumn<string>(
                name: "CriteriaText",
                table: "RubricCriteria",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Faculty",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Faculty",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RubricCriteriaElement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RubricCriteriaId1 = table.Column<int>(nullable: true),
                    RubricCriteriaId = table.Column<string>(nullable: true),
                    CriteriaText = table.Column<string>(nullable: true),
                    ScoreValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubricCriteriaElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RubricCriteriaElement_RubricCriteria_RubricCriteriaId1",
                        column: x => x.RubricCriteriaId1,
                        principalTable: "RubricCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RubricCriteriaElement_RubricCriteriaId1",
                table: "RubricCriteriaElement",
                column: "RubricCriteriaId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RubricCriteriaElement");

            migrationBuilder.DropColumn(
                name: "CriteriaText",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Faculty");

            migrationBuilder.AddColumn<string>(
                name: "Desciption0",
                table: "RubricCriteria",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desciption1",
                table: "RubricCriteria",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desciption2",
                table: "RubricCriteria",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desciption3",
                table: "RubricCriteria",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desciption4",
                table: "RubricCriteria",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RubricCriteria",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Faculty",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: true);
        }
    }
}
