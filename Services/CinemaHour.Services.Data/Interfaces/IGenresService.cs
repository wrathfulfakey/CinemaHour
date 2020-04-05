namespace CinemaHour.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IGenresService
    {
        ICollection<T> GetAll<T>(int? count = null);

        ICollection<T> GetAllMovies<T>(int id);

        T GetById<T>(int id);
    }
}
