using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectGovDash.Migrations
{
    /// <inheritdoc />
    public partial class PublicWiFi_DateofInstallation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateofInstallation",
                table: "PublicWiFiDbset",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateofInstallation",
                table: "PublicWiFiDbset");
        }
    }
}
