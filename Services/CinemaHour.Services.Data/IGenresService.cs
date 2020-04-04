using System.Collections.Generic;

namespace CinemaHour.Services.Data
{
    public interface IGenresService
    {
        ICollection<T> GetAll<T>(int? count = null);
    }
}
