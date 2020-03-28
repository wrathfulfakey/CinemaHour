using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaHour.Data.Migrations
{
    public partial class InfoAboutActors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Actors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Info",
                table: "Actors");
        }
    }
}
