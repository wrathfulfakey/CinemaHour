namespace CinemaHour.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.ViewModels.Movies;

    public interface IMoviesService
    {
        ICollection<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);

        Task<int> CreaterMovieAsync(CreateMovieServiceInputModel input);

        Task DeleteMovieAsync(int id);

        Task HardDeleteMovieAsync(int id);

        Task EditActorAsync(EditMovieInputModel input);

        Task<string> AddMovieToWatched(int movieId, string username);

        Task<string> AddMovieToFavourites(int movieId, string username);
    }
}
