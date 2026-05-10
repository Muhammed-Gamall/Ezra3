using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation.Data.Mirations
{
    /// <inheritdoc />
    public partial class somefixespart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "Bundles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OriginalPrice",
                table: "Bundles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
