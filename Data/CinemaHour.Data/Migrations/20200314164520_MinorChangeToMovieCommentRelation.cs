namespace CinemaHour.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MinorChangeToMovieCommentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Movies_MovieId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_MovieComments_CommentId",
                table: "MovieComments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MovieId",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_CommentId",
                table: "MovieComments",
                column: "CommentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieComments_CommentId",
                table: "MovieComments");

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_CommentId",
                table: "MovieComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MovieId",
                table: "Comments",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Movies_MovieId",
                table: "Comments",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
