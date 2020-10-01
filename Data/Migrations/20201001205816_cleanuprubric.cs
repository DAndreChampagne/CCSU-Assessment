using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class cleanuprubric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Criterion01",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion02",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion03",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion04",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion05",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion06",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion07",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion08",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion09",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterion10",
                table: "Rubrics",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
