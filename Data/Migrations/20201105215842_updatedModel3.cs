using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class updatedModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Artifacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Artifacts",
                type: "varchar(256) CHARACTER SET utf8mb4",
                maxLength: 256,
                nullable: true);
        }
    }
}
