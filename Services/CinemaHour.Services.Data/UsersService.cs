namespace CinemaHour.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IRepository<UserFavourite> userFavouriteRepository;
        private readonly IRepository<UserWatched> userWatchedRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Movie> moviesRepository,
            IRepository<UserFavourite> userFavouriteRepository,
            IRepository<UserWatched> userWatchedRepository)
        {
            this.usersRepository = usersRepository;
            this.moviesRepository = moviesRepository;
            this.userFavouriteRepository = userFavouriteRepository;
            this.userWatchedRepository = userWatchedRepository;
        }

        public T GetById<T>(string username)
        {
            var user = this.usersRepository.All()
                .Where(x => x.UserName == username)
                .To<T>()
                .FirstOrDefault();

            return user;
        }

        public async Task<string> RemoveFromFavouritesAsync(int movieId, string username)
        {
            var user = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            var targetMovieToRemove = this.userFavouriteRepository.All()
                .Where(x => x.MovieId == movieId && x.ApplicationUserId == user.Id)
                .FirstOrDefault();

            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);

            if (targetMovieToRemove == null)
            {
                return $"You don't have this movie in your favourites list.";
            }

            user.Favourites.Remove(targetMovieToRemove);

            await this.usersRepository.SaveChangesAsync();

            return $"You successfully removed {movie.Name} from your favourites list.";
        }

        public async Task<string> RemoveFromWatchedAsync(int movieId, string username)
        {
            var user = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            var targetMovieToRemove = this.userWatchedRepository.All()
                .Where(x => x.MovieId == movieId && x.ApplicationUserId == user.Id)
                .FirstOrDefault();

            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);

            if (targetMovieToRemove == null)
            {
                return $"You don't have this movie in your watched list.";
            }

            user.TotalTimeWatched -= movie.Length;

            user.Watched.Remove(targetMovieToRemove);

            await this.usersRepository.SaveChangesAsync();

            return $"You successfully removed {targetMovieToRemove.Movie.Name} from your favourites list.";
        }
    }
}
