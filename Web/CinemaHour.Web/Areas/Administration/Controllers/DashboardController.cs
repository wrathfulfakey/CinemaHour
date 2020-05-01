namespace CinemaHour.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CinemaHour.Common;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IMoviesService moviesService;
        private readonly IUsersService usersService;

        public DashboardController(
            ISettingsService settingsService,
            IMoviesService moviesService,
            IUsersService usersService)
        {
            this.settingsService = settingsService;
            this.moviesService = moviesService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                SettingsCount = this.settingsService.GetCount(),
            };

            return this.View(viewModel);
        }

        public IActionResult Movies()
        {
            var viewModel = new AllMoviesViewModel
            {
                Movies = this.moviesService.GetAllWithDeleted<MoviesViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> RecoverMovie(int movieId)
        {
            await this.moviesService.RecoverMovie(movieId);

            return this.RedirectToAction(nameof(this.Movies));
        }

        public IActionResult NewUsers()
        {
            var viewModel = new AllNewUsersViewModel
            {
                Users = this.usersService.GetAllWithDeleted<NewUserViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
