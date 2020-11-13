using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class modelchanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RubricCriteriaElement_RubricCriteria_RubricCriteriaId1",
                table: "RubricCriteriaElement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RubricCriteriaElement",
                table: "RubricCriteriaElement");

            migrationBuilder.RenameTable(
                name: "RubricCriteriaElement",
                newName: "RubricCriteriaElements");

            migrationBuilder.RenameIndex(
                name: "IX_RubricCriteriaElement_RubricCriteriaId1",
                table: "RubricCriteriaElements",
                newName: "IX_RubricCriteriaElements_RubricCriteriaId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RubricCriteriaElements",
                table: "RubricCriteriaElements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RubricCriteriaElements_RubricCriteria_RubricCriteriaId1",
                table: "RubricCriteriaElements",
                column: "RubricCriteriaId1",
                principalTable: "RubricCriteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RubricCriteriaElements_RubricCriteria_RubricCriteriaId1",
                table: "RubricCriteriaElements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RubricCriteriaElements",
                table: "RubricCriteriaElements");

            migrationBuilder.RenameTable(
                name: "RubricCriteriaElements",
                newName: "RubricCriteriaElement");

            migrationBuilder.RenameIndex(
                name: "IX_RubricCriteriaElements_RubricCriteriaId1",
                table: "RubricCriteriaElement",
                newName: "IX_RubricCriteriaElement_RubricCriteriaId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RubricCriteriaElement",
                table: "RubricCriteriaElement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RubricCriteriaElement_RubricCriteria_RubricCriteriaId1",
                table: "RubricCriteriaElement",
                column: "RubricCriteriaId1",
                principalTable: "RubricCriteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
