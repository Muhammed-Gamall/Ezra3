using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation.Data.Migrations
{
    /// <inheritdoc />
    public partial class addAuditableEntityForFarmerRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "FarmerRatings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "FarmerRatings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "FarmerRatings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "FarmerRatings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FarmerRatings_CreatedById",
                table: "FarmerRatings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FarmerRatings_UpdatedById",
                table: "FarmerRatings",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_FarmerRatings_AspNetUsers_CreatedById",
                table: "FarmerRatings",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FarmerRatings_AspNetUsers_UpdatedById",
                table: "FarmerRatings",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FarmerRatings_AspNetUsers_CreatedById",
                table: "FarmerRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_FarmerRatings_AspNetUsers_UpdatedById",
                table: "FarmerRatings");

            migrationBuilder.DropIndex(
                name: "IX_FarmerRatings_CreatedById",
                table: "FarmerRatings");

            migrationBuilder.DropIndex(
                name: "IX_FarmerRatings_UpdatedById",
                table: "FarmerRatings");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "FarmerRatings");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "FarmerRatings");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "FarmerRatings");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "FarmerRatings");
        }
    }
}
