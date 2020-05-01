namespace CinemaHour.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;

    public interface IUsersService
    {
        T GetByUsername<T>(string username);

        ICollection<T> GetAllWithDeleted<T>();

        Task<string> RemoveFromFavouritesAsync(int movieId, string username);

        Task<string> RemoveFromWatchedAsync(int movieId, string username);

        Task<string> MakeUserAdminAsync(string username);

        Task<string> LockdownUserAsync(string username);

        Task<string> RemoveLockdownUserAsync(string username);

        Task<string> DeleteUserAsync(string username);

        Task<string> AddMovieToFavouritesAsync(int movieId, string username);

        Task<string> AddMovieToWatchedAsync(int movieId, string username);
    }
}
