using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DownNotifier.API.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserIdToBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AplicationUserId",
                table: "TargetApp",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AplicationUserId",
                table: "ApplicationUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "AplicationUserId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "AplicationUserId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "AplicationUserId",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AplicationUserId",
                table: "TargetApp");

            migrationBuilder.DropColumn(
                name: "AplicationUserId",
                table: "ApplicationUser");
        }
    }
}
