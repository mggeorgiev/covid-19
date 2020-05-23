using Microsoft.EntityFrameworkCore.Migrations;

namespace covid_19.Data.Migrations
{
    public partial class CreateAllView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE VIEW[AllUnique] AS " +
                                        "SELECT* FROM[dbo].[All] " +
                                        "WHERE [Date] in " +
                                        "(SELECT TOP (1000) " +
                                                "Max([Date]) " +
                                                "FROM[covid19].[dbo].[All] " +
                                                "GROUP BY YEAR([Date]) " +
                                                ",Month([Date]) " +
                                                ",DAy([Date]))" +
                                                "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[AllUnique]");
        }
    }
}
