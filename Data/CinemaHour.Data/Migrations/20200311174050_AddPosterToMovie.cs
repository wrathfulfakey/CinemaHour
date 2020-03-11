using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaHour.Data.Migrations
{
    public partial class AddPosterToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Poster",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Movies");
        }
    }
}
