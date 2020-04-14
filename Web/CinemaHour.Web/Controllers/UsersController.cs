namespace CinemaHour.Web.Controllers
{
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
    }
}
