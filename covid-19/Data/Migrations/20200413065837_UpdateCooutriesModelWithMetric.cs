using Microsoft.EntityFrameworkCore.Migrations;

namespace covid_19.Data.Migrations
{
    public partial class UpdateCooutriesModelWithMetric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "casesPerOneMillion",
                table: "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "deathsPerOneMillion",
                table: "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "testsPerOneMillion",
                table: "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "totalTests",
                table: "Countries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "casesPerOneMillion",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "deathsPerOneMillion",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "testsPerOneMillion",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "totalTests",
                table: "Countries");
        }
    }
}
