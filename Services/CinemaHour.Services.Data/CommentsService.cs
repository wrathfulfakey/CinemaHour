namespace CinemaHour.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(
            IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int movieId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                MovieId = movieId,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var commentMovie = this.commentsRepository.All().Where(x => x.Id == id)
                .FirstOrDefault();

            commentMovie.Content = "<p style=\"color:red;\">This user has deleted this comment.<p>";

            this.commentsRepository.Update(commentMovie);
            await this.commentsRepository.SaveChangesAsync();

            return commentMovie.MovieId;
        }

        public bool IsInMovieId(int commentId, int movieId)
        {
            var commentMovieId = this.commentsRepository.All().Where(x => x.Id == commentId)
                .Select(x => x.MovieId).FirstOrDefault();

            return commentMovieId == movieId;
        }
    }
}
