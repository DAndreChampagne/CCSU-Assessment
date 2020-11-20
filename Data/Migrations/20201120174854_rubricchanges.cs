using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class rubricchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RubricCriteria_Rubrics_RubricId",
                table: "RubricCriteria");

            migrationBuilder.DropForeignKey(
                name: "FK_RubricCriteriaElements_RubricCriteria_RubricCriteriaId1",
                table: "RubricCriteriaElements");

            migrationBuilder.DropIndex(
                name: "IX_RubricCriteriaElements_RubricCriteriaId1",
                table: "RubricCriteriaElements");

            migrationBuilder.DropIndex(
                name: "IX_RubricCriteria_RubricId",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "RubricCriteriaId1",
                table: "RubricCriteriaElements");

            migrationBuilder.AlterColumn<int>(
                name: "RubricCriteriaId",
                table: "RubricCriteriaElements",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RubricId",
                table: "RubricCriteria",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RubricId1",
                table: "RubricCriteria",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RubricCriteriaElements_RubricCriteriaId",
                table: "RubricCriteriaElements",
                column: "RubricCriteriaId");

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
                name: "FK_RubricCriteriaElements_RubricCriteria_RubricCriteriaId",
                table: "RubricCriteriaElements",
                column: "RubricCriteriaId",
                principalTable: "RubricCriteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RubricCriteria_Rubrics_RubricId1",
                table: "RubricCriteria");

            migrationBuilder.DropForeignKey(
                name: "FK_RubricCriteriaElements_RubricCriteria_RubricCriteriaId",
                table: "RubricCriteriaElements");

            migrationBuilder.DropIndex(
                name: "IX_RubricCriteriaElements_RubricCriteriaId",
                table: "RubricCriteriaElements");

            migrationBuilder.DropIndex(
                name: "IX_RubricCriteria_RubricId1",
                table: "RubricCriteria");

            migrationBuilder.DropColumn(
                name: "RubricId1",
                table: "RubricCriteria");

            migrationBuilder.AlterColumn<string>(
                name: "RubricCriteriaId",
                table: "RubricCriteriaElements",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RubricCriteriaId1",
                table: "RubricCriteriaElements",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RubricId",
                table: "RubricCriteria",
                type: "varchar(2) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_RubricCriteriaElements_RubricCriteriaId1",
                table: "RubricCriteriaElements",
                column: "RubricCriteriaId1");

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
                name: "FK_RubricCriteriaElements_RubricCriteria_RubricCriteriaId1",
                table: "RubricCriteriaElements",
                column: "RubricCriteriaId1",
                principalTable: "RubricCriteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
