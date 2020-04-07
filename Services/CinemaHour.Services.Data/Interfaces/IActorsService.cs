namespace CinemaHour.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.ViewModels.Actors;

    public interface IActorsService
    {
        ICollection<T> GetAll<T>(int? count = null);

        T GetById<T>(string id);

        Task EditActorAsync(EditActorInputModel input);

        Task<string> CreateActorAsync(CreateActorViewModel input);

        Task DeleteActorAsync(string id);

        Task HardDeleteActorAsync(string id);
    }
}
