namespace CinemaHour.Web.Controllers
{
    using CinemaHour.Services.Data;
    using CinemaHour.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public IActionResult All()
        {
            var viewModel = new AllMoviesViewModel
            {
                Movies = this.moviesService.GetAll<MovieViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var movieViewModel = this.moviesService
                .GetById<MovieDetailsViewModel>(id);

            if (movieViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(movieViewModel);
        }
    }
}
