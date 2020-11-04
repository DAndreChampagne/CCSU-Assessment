using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Data.Migrations
{
    public partial class renamerubriccode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name:"Code", 
                table:"Rubrics", 
                newName:"Abbreviation");
            
            
            // migrationBuilder.DropColumn(
            //     name: "Code",
            //     table: "Rubrics");

            // migrationBuilder.AddColumn<string>(
            //     name: "Abbreviation",
            //     table: "Rubrics",
            //     maxLength: 2,
            //     nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "Rubrics");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Rubrics",
                type: "varchar(2) CHARACTER SET utf8mb4",
                maxLength: 2,
                nullable: true);
        }
    }
}
