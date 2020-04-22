namespace CinemaHour.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

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
