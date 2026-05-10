using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation.Data.Mirations
{
    /// <inheritdoc />
    public partial class BundleAndBlogupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashCode",
                table: "Bundles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashCode",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashCode",
                table: "Bundles");

            migrationBuilder.DropColumn(
                name: "HashCode",
                table: "Blogs");
        }
    }
}
