using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Graduation.Data.Mirations
{
    /// <inheritdoc />
    public partial class AddingRolesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "302BC17E-FB4C-4B60-9416-FA950FA0637C", "2C10A9FD-1107-4FD3-B528-F59C0828F1BC", false, false, "Admin", "ADMIN" },
                    { "7EE38D0B-020D-482C-BEF4-4CA0CDBE600C", "29D8E153-A79C-44B1-94EE-3EF9ECDC27EC", false, false, "Farmer", "FARMER" },
                    { "FB3D7421-BDBD-42D8-935E-79D6A1B1BDC6", "7D072CE6-2AB9-4EAA-BA2E-22323C7B7972", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsActive", "IsCasualUser", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "62742CE5-5674-4581-B7D2-72A6156AB908", 0, "953E4CA8-C9DA-45B1-A470-B4ECD0FD5825", "admin@Ezr3.com", true, "Admin User", true, false, false, null, "ADMIN@EZR3.COM", "ADMIN@EZR3.COM", "AQAAAAIAAYagAAAAECT5qqExszEq4/R10c5+9427qkSJA5NH2Ei8cAyBmngHLCSAFaMdsDrjM8AXTXvrYg==", null, false, "2E483331-1D09-4A82-86B6-62FCBBFBCA8A", false, "admin@Ezr3.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "302BC17E-FB4C-4B60-9416-FA950FA0637C", "62742CE5-5674-4581-B7D2-72A6156AB908" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7EE38D0B-020D-482C-BEF4-4CA0CDBE600C");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "FB3D7421-BDBD-42D8-935E-79D6A1B1BDC6");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "302BC17E-FB4C-4B60-9416-FA950FA0637C", "62742CE5-5674-4581-B7D2-72A6156AB908" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "302BC17E-FB4C-4B60-9416-FA950FA0637C");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62742CE5-5674-4581-B7D2-72A6156AB908");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetRoles");
        }
    }
}
