namespace CinemaHour.Web.Controllers
{
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels.Genres;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class GenresController : Controller
    {
        private readonly IGenresService genresService;
        private readonly IMoviesService moviesService;

        public GenresController(
            IGenresService genresService,
            IMoviesService moviesService)
        {
            this.genresService = genresService;
            this.moviesService = moviesService;
        }

        public IActionResult All()
        {
            var viewModel = new AllGenresViewModel
            {
                Genres = this.genresService.GetAll<GenreViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var viewModel = new DetailsGenreViewModel
            {
                Movies = this.genresService.GetAllMovies<MovieDetailsGenreViewModel>(id),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
