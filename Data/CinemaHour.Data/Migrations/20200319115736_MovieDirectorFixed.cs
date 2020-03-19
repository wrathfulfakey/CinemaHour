using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaHour.Data.Migrations
{
    public partial class MovieDirectorFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieDirectors_MovieId",
                table: "MovieDirectors");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DirectorId",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieDirectors_MovieId",
                table: "MovieDirectors",
                column: "MovieId",
                unique: true);
        }
    }
}
