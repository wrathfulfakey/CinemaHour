namespace CinemaHour.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;

    public interface IUsersService
    {
        T GetById<T>(string username);

        Task<string> RemoveFromFavouritesAsync(int movieId, string username);

        Task<string> RemoveFromWatchedAsync(int movieId, string username);
    }
}
