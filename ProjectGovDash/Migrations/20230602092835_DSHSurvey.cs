using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectGovDash.Migrations
{
    /// <inheritdoc />
    public partial class DSHSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DSHSurveyDbset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofCreation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateofModification = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofSurvey = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoLastMileSites = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceLastMile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceMetro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistanceBackbone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DSHSurveyDbset", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DSHSurveyDbset");
        }
    }
}
