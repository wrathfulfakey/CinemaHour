namespace CinemaHour.Web.Controllers
{
    using System.Threading.Tasks;

    using CinemaHour.Common;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> MakeAdmin(string username)
        {
            var result = await this.usersService.MakeUserAdminAsync(username);

            this.TempData["AddAdminRoleToUser"] = result;

            return this.RedirectToAction(nameof(this.Profile), new { username });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> LockdownUser(string username)
        {
            var result = await this.usersService.LockdownUserAsync(username);

            this.TempData["LockdownUser"] = result;

            return this.RedirectToAction(nameof(this.Profile), new { username });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> RemoveLockdownUser(string username)
        {
            var result = await this.usersService.RemoveLockdownUserAsync(username);

            this.TempData["RemoveLockdownUser"] = result;

            return this.RedirectToAction(nameof(this.Profile), new { username });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var result = await this.usersService.DeleteUserAsync(username);

            this.TempData["DeleteUser"] = result;

            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromWatched(int movieId)
        {
            var user = this.User.Identity.Name;

            var result = await this.usersService.RemoveFromWatchedAsync(movieId, user);

            this.TempData["RemoveMovieFromWatched"] = result;

            return this.RedirectToAction(nameof(this.Profile), new { username = user });
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromFavourites(int movieId)
        {
            var user = this.User.Identity.Name;

            var result = await this.usersService.RemoveFromFavouritesAsync(movieId, user);

            this.TempData["RemoveMovieFromFavourites"] = result;

            return this.RedirectToAction(nameof(this.Profile), new { username = user });
        }

        [Authorize]
        public async Task<IActionResult> AddToUserFavourites(int movieId)
        {
            var result = await this.usersService.AddMovieToFavouritesAsync(movieId, this.User.Identity.Name);

            this.TempData["UserAddToFavourite"] = result;

            return this.Redirect($"/Movies/Details?id={movieId}");
        }

        [Authorize]
        public async Task<IActionResult> AddToUserWatched(int movieId)
        {
            var result = await this.usersService.AddMovieToWatchedAsync(movieId, this.User.Identity.Name);

            this.TempData["UserAddToWatched"] = result;

            return this.Redirect($"/Movies/Details?id={movieId}");
        }
    }
}
