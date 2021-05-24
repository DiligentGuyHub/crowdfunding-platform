using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdfunding_application.Data.Migrations
{
    public partial class updatedNewsAddedIsFinished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "News");
        }
    }
}
