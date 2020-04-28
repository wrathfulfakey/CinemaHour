namespace CinemaHour.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Common;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels.Genres;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : Controller
    {
        private const int MoviesPerPageDefaultValue = 12;

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

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult Details(
            int id,
            int page = 1,
            int perPage = MoviesPerPageDefaultValue)
        {
            var genre = this.genresService.GetById<GenreViewModel>(id);

            var movies = this.genresService
                .GetAllMovies<MovieDetailsGenreViewModel>(id);

            var pagesCount = (int)Math.Ceiling(movies.Count() / (decimal)perPage);

            movies = movies
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .ToList();

            var viewModel = new DetailsGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                Movies = movies,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var genre = this.genresService
                .GetById<GenreEditViewModel>(id);

            if (genre == null)
            {
                return this.NotFound();
            }

            return this.View(genre);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GenreEditViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var genreId = await this.genresService.EditGenreAsync(input.Id, input.Name);

            return this.RedirectToAction(nameof(this.Details), new { id = genreId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        public async Task<IActionResult> Create(CreateGenreViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.genresService.CreateGenreAsync(input.Name);

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.genresService.DeleteGenreAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> HardDelete(int id)
        {
            await this.genresService.HardDeleteGenreAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
