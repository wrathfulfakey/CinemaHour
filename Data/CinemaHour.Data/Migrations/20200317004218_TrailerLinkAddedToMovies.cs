using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaHour.Data.Migrations
{
    public partial class TrailerLinkAddedToMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrailerLink",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrailerLink",
                table: "Movies");
        }
    }
}
