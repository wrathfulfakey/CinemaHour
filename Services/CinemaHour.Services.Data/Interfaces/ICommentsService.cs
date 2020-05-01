namespace CinemaHour.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(int movieId, string userId, string content, int? parentId = null);

        Task<int> Delete(int id);

        bool IsInMovieId(int commentId, int movieId);

        bool CommentByUserId(string userId, int commentId);
    }
}
