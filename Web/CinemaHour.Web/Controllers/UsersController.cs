namespace CinemaHour.Web.Controllers
{
    using System.Threading.Tasks;

    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Profile(string username)
        {
            var user = this.usersService.GetById<UserViewModel>(username);

            if (user == null)
            {
                return this.NotFound();
            }

            return this.View(user);
        }

        public async Task<IActionResult> RemoveFromWatched(int movieId)
        {
            var user = this.User.Identity.Name;

            var result = await this.usersService.RemoveFromWatchedAsync(movieId, user);

            this.TempData["RemoveMovieFromWatched"] = result;

            return this.RedirectToAction(nameof(this.Profile), new { username = user });
        }

        public async Task<IActionResult> RemoveFromFavourites(int movieId)
        {
            var user = this.User.Identity.Name;

            var result = await this.usersService.RemoveFromFavouritesAsync(movieId, user);

            this.TempData["RemoveMovieFromFavourites"] = result;

            return this.RedirectToAction(nameof(this.Profile), new { username = user });
        }
    }
}
