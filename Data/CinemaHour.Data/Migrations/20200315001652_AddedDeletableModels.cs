namespace CinemaHour.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedDeletableModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IMDBLink",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Movies",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Directors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Directors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Actors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Actors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Directors_IsDeleted",
                table: "Directors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_IsDeleted",
                table: "Actors",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Directors_IsDeleted",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_IsDeleted",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "IMDBLink",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Actors");
        }
    }
}
