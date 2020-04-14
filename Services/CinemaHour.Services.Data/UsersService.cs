namespace CinemaHour.Services.Data
{
    using System.Linq;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public T GetById<T>(string username)
        {
            var user = this.userRepository.All()
                .Where(x => x.UserName == username)
                .To<T>()
                .FirstOrDefault();

            return user;
        }
    }
}
