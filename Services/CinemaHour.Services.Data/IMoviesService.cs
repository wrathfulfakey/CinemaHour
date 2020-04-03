namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;

    public interface IMoviesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);
    }
}
