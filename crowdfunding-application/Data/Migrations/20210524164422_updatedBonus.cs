using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdfunding_application.Data.Migrations
{
    public partial class updatedBonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_Campaigns_CampaignId",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "CampaingId",
                table: "Bonuses");

            migrationBuilder.AlterColumn<int>(
                name: "CampaignId",
                table: "Bonuses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_Campaigns_CampaignId",
                table: "Bonuses",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_Campaigns_CampaignId",
                table: "Bonuses");

            migrationBuilder.AlterColumn<int>(
                name: "CampaignId",
                table: "Bonuses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CampaingId",
                table: "Bonuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_Campaigns_CampaignId",
                table: "Bonuses",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
