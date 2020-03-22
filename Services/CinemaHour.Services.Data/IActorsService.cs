namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;

    using CinemaHour.Data.Models;

    public interface IActorsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Actor GetById(string id);
    }
}
