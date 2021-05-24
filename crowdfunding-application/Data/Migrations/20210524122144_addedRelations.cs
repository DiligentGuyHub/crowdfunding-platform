using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdfunding_application.Data.Migrations
{
    public partial class addedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CampaignId",
                table: "Transactions",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CampaignId",
                table: "Tags",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CampaignId",
                table: "News",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Campaigns_CampaignId",
                table: "News",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Campaigns_CampaignId",
                table: "Tags",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Campaigns_CampaignId",
                table: "Transactions",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Campaigns_CampaignId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Campaigns_CampaignId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Campaigns_CampaignId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CampaignId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CampaignId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_News_CampaignId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Tags");
        }
    }
}
