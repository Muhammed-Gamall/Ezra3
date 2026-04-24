using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation.Data.Mirations
{
    /// <inheritdoc />
    public partial class updatingOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_FarmerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_FarmerProfiles_FarmerProfileUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FarmerProfileUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FarmerProfileUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_FarmerProfiles_FarmerId",
                table: "Orders",
                column: "FarmerId",
                principalTable: "FarmerProfiles",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_FarmerProfiles_FarmerId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "FarmerProfileUserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FarmerProfileUserId",
                table: "Orders",
                column: "FarmerProfileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_FarmerId",
                table: "Orders",
                column: "FarmerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_FarmerProfiles_FarmerProfileUserId",
                table: "Orders",
                column: "FarmerProfileUserId",
                principalTable: "FarmerProfiles",
                principalColumn: "UserId");
        }
    }
}
