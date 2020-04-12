namespace CinemaHour.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDirectorsService
    {
        ICollection<T> GetAll<T>(int? count = null);

        Task<string> EditDirectorAsync(string id, string directorFullName);

        T GetById<T>(string id);

        Task<string> CreateDirectorAsync(string fullName);

        ICollection<T> GetAllMovies<T>(string id);

        Task DeleteDirectorAsync(string id);

        Task HardDeleteDirectorAsync(string id);
    }
}
