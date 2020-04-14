namespace CinemaHour.Services.Data.Interfaces
{
    using CinemaHour.Data.Models;

    public interface IUsersService
    {
        T GetById<T>(string username);
    }
}
