using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdfunding_application.Data.Migrations
{
    public partial class updatedCampaignAndNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "News");

            migrationBuilder.AddColumn<int>(
                name: "MoneyActual",
                table: "Campaigns",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyActual",
                table: "Campaigns");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
