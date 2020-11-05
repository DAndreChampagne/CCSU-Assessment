using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class updatedModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "RubricId",
                table: "Scores",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RubricId",
                table: "RubricCriteria",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RubricId",
                table: "Scores",
                column: "RubricId");

            migrationBuilder.CreateIndex(
                name: "IX_RubricCriteria_RubricId",
                table: "RubricCriteria",
                column: "RubricId");

            migrationBuilder.AddForeignKey(
                name: "FK_RubricCriteria_Rubrics_RubricId",
                table: "RubricCriteria",
                column: "RubricId",
                principalTable: "Rubrics",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<int>(
                name: "RubricId",
                table: "Scores",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RubricId1",
                table: "Scores",
                type: "varchar(2) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RubricId",
                table: "RubricCriteria",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RubricId1",
                table: "RubricCriteria",
                type: "varchar(2) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RubricId1",
                table: "Scores",
                column: "RubricId1");

            migrationBuilder.CreateIndex(
                name: "IX_RubricCriteria_RubricId1",
                table: "RubricCriteria",
                column: "RubricId1");

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
    }
}
