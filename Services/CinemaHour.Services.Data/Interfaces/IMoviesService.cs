namespace CinemaHour.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CinemaHour.Services.Data.ViewModels.Movies;

    public interface IMoviesService
    {
        ICollection<T> GetAll<T>(int? count = null);

        ICollection<T> GetAllWithDeleted<T>();

        Task RecoverMovie(int movieId);

        T GetById<T>(int id);

        Task<int> CreaterMovieAsync(CreateMovieServiceInputModel input);

        Task DeleteMovieAsync(int id);

        Task HardDeleteMovieAsync(int id);

        Task EditActorAsync(EditMovieInputModel input);
    }
}
