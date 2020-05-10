namespace CinemaHour.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IRepository<UserFavourite> userFavouriteRepository;
        private readonly IRepository<UserWatched> userWatchedRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Movie> moviesRepository,
            IRepository<UserFavourite> userFavouriteRepository,
            IRepository<UserWatched> userWatchedRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.usersRepository = usersRepository;
            this.moviesRepository = moviesRepository;
            this.userFavouriteRepository = userFavouriteRepository;
            this.userWatchedRepository = userWatchedRepository;
            this.userManager = userManager;
        }

        public T GetByUsername<T>(string username)
        {
            var user = this.usersRepository.All()
                .Where(x => x.UserName == username)
                .To<T>()
                .FirstOrDefault();

            return user;
        }

        public async Task<string> LockdownUserAsync(string username)
        {
            var user = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            if (user == null)
            {
                return "User not found";
            }

            if (await this.userManager.IsInRoleAsync(user, "Administrator"))
            {
                return "You cannot ban another administrator";
            }

            if (user.LockoutEnabled && user.LockoutEnd != null)
            {
                return $"'{user.UserName}' is already banned until {user.LockoutEnd.Value:yyyy/MMM/dd/HH}";
            }

            if (user.LockoutEnd == null || user.LockoutEnd <= DateTime.UtcNow)
            {
                user.LockoutEnd = DateTime.UtcNow.AddDays(7);
                user.LockoutEnabled = true;
                await this.usersRepository.SaveChangesAsync();
            }

            await this.usersRepository.SaveChangesAsync();

            return $"You have successfully banned '{user.UserName}' for 7 days. His ban will expire on {user.LockoutEnd.Value:yyyy/MMM/dd/HH}";
        }

        public async Task<string> RemoveLockdownUserAsync(string username)
        {
            var user = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            if (user == null)
            {
                return "User not found";
            }

            if (!user.LockoutEnabled)
            {
                return "The user is not currently banned.";
            }

            user.LockoutEnd = null;
            user.LockoutEnabled = false;

            await this.usersRepository.SaveChangesAsync();

            return $"You successfully removed the ban for '{user.UserName}'";
        }

        public async Task<string> MakeUserAdminAsync(string username)
        {
            var user = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            if (user == null)
            {
                return "User not found";
            }

            if (await this.userManager.IsInRoleAsync(user, "Administrator"))
            {
                return $"This user is already an administrator.";
            }

            await this.userManager.AddToRoleAsync(user, "Administrator");

            return $"Administrator role has successfully been added to '{user.UserName}'.";
        }

        public async Task<string> DeleteUserAsync(string username)
        {
            var user = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            if (user == null)
            {
                return "User not found.";
            }

            if (await this.userManager.IsInRoleAsync(user, "Administrator"))
            {
                return "You cannot delete another Administrator";
            }

            string name = user.UserName;

            this.usersRepository.Delete(user);
            await this.usersRepository.SaveChangesAsync();
            return $"User '{name}' successfully deleted.";
        }

        public async Task<string> RecoverUserAsync(string username)
        {
            var user = this.usersRepository
                .AllAsNoTrackingWithDeleted()
                .Where(x => x.UserName == username)
                .FirstOrDefault();

            if (user == null)
            {
                return "User not found.";
            }

            string name = user.UserName;

            user.IsDeleted = false;

            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();

            return $"User '{name}' successfully recovered.";
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

            return $"You successfully removed '{movie.Name}' from your favourites list.";
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

            return $"You successfully removed '{movie.Name}' from your favourites list.";
        }

        public async Task<string> AddMovieToWatchedAsync(int movieId, string username)
        {
            var user = this.usersRepository.All()
                .Where(x => x.UserName == username)
                .FirstOrDefault();

            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);

            var checkIfExists = this.userWatchedRepository.All()
                .Where(x => x.ApplicationUserId == user.Id && x.MovieId == movieId)
                .FirstOrDefault();

            user.Watched = this.userWatchedRepository.All().Where(x => x.ApplicationUserId == user.Id).ToList();

            if (checkIfExists != null)
            {
                return $"You already have '{movie.Name}' in your watched list.";
            }

            var userWatched = new UserWatched
            {
                ApplicationUserId = user.Id,
                MovieId = movieId,
            };

            user.Watched.Add(userWatched);

            user.TotalTimeWatched += movie.Length;

            await this.usersRepository.SaveChangesAsync();

            return $"You have successfully added '{movie.Name}' to your watched list.";
        }

        public async Task<string> AddMovieToFavouritesAsync(int movieId, string username)
        {
            var user = this.usersRepository.All()
                .Where(x => x.UserName == username)
                .FirstOrDefault();

            var checkIfExists = this.userFavouriteRepository.All()
                .Where(x => x.ApplicationUserId == user.Id && x.MovieId == movieId)
                .FirstOrDefault();

            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);

            if (checkIfExists != null)
            {
                return $"You already have '{movie.Name}' in your favourites list.";
            }

            var userFavourite = new UserFavourite
            {
                ApplicationUserId = user.Id,
                MovieId = movieId,
            };

            user.Favourites.Add(userFavourite);

            await this.usersRepository.SaveChangesAsync();

            return $"You have successfully added '{movie.Name}' to your favourites list.";
        }

        public ICollection<T> GetAllWithDeleted<T>()
        {
            IQueryable<ApplicationUser> query = this.usersRepository
               .AllAsNoTrackingWithDeleted()
               .Where(x => x.CreatedOn >= DateTime.UtcNow.AddDays(-30))
               .OrderByDescending(x => x.CreatedOn);

            return query.To<T>().ToList();
        }
    }
}
