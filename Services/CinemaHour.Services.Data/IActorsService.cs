namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.ViewModels.Actors;

    public interface IActorsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(string id);

        Task<string> CreateActorAsync(CreateActorViewModel input);
    }
}
