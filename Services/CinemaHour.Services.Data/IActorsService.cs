namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;

    public interface IActorsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
