namespace CinemaHour.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenresService
    {
        ICollection<T> GetAll<T>(int? count = null);

        ICollection<T> GetAllMovies<T>(int id);

        T GetById<T>(int id);

        Task<int> EditGenreAsync(int id, string genreName);

        Task DeleteGenreAsync(int id);

        Task HardDeleteGenreAsync(int id);
    }
}
