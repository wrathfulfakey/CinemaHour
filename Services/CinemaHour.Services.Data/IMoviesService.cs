namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.ViewModels.Movies;

    public interface IMoviesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);

        Task<int> CreaterMovieAsync(CreateMovieServiceInputModel input);
    }
}
