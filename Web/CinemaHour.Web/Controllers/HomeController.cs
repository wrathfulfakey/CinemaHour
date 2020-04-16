namespace CinemaHour.Web.Controllers
{
    using System.Diagnostics;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels;
    using CinemaHour.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IActorsService actorsService;
        private readonly IMoviesService moviesService;

        public HomeController(
            IActorsService actorsService,
            IMoviesService moviesService)
        {
            this.actorsService = actorsService;
            this.moviesService = moviesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Actors = this.actorsService.GetAll<IndexActorsViewModel>(3),
                Movies = this.moviesService.GetAll<IndexMoviesViewModel>(6),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
