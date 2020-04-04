namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;

    public interface IDirectorsService
    {
        ICollection<T> GetAll<T>(int? count = null);
    }
}
