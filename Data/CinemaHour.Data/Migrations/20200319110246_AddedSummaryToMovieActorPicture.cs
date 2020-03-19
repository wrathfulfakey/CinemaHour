using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaHour.Data.Migrations
{
    public partial class AddedSummaryToMovieActorPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Actors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Actors");
        }
    }
}
