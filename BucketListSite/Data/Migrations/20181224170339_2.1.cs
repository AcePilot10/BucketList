using Microsoft.EntityFrameworkCore.Migrations;

namespace BucketListSite.Data.Migrations
{
    public partial class _21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketListItems_AspNetUsers_UserId",
                table: "BucketListItems");

            migrationBuilder.DropIndex(
                name: "IX_BucketListItems_UserId",
                table: "BucketListItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BucketListItems");

            migrationBuilder.AddColumn<string>(
                name: "BucketListItemsJson",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BucketListItemsJson",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BucketListItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BucketListItems_UserId",
                table: "BucketListItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BucketListItems_AspNetUsers_UserId",
                table: "BucketListItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
