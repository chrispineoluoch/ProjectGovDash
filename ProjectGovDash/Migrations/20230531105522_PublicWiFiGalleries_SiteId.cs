using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectGovDash.Migrations
{
    /// <inheritdoc />
    public partial class PublicWiFiGalleries_SiteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SiteId",
                table: "PublicWiFiGalleryDbset",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "PublicWiFiGalleryDbset",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
