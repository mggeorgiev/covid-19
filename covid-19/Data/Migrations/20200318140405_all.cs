using Microsoft.EntityFrameworkCore.Migrations;

namespace covid_19.Data.Migrations
{
    public partial class all : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "All",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cases = table.Column<int>(nullable: false),
                    Deaths = table.Column<int>(nullable: false),
                    Recovered = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_All", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Cases = table.Column<int>(nullable: false),
                    TodayCases = table.Column<int>(nullable: false),
                    Deaths = table.Column<int>(nullable: false),
                    TodayDeaths = table.Column<int>(nullable: false),
                    Recovered = table.Column<int>(nullable: false),
                    Active = table.Column<int>(nullable: false),
                    Critical = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "All");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
