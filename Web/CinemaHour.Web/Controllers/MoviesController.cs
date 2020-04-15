namespace CinemaHour.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Common;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Data.ViewModels.Movies;
    using CinemaHour.Web.ViewModels.Movies;
    using CinemaHour.Web.ViewModels.Movies.Edit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : Controller
    {
        private const int MoviesPerPageDefaultValue = 15;

        private readonly IMoviesService moviesService;
        private readonly IActorsService actorsService;
        private readonly IGenresService genresService;
        private readonly IDirectorsService directorsService;

        public MoviesController(
            IMoviesService moviesService,
            IActorsService actorsService,
            IGenresService genresService,
            IDirectorsService directorsService)
        {
            this.moviesService = moviesService;
            this.actorsService = actorsService;
            this.genresService = genresService;
            this.directorsService = directorsService;
        }

        public IActionResult All(
            string searchString,
            string currentFilter,
            string sortOrder,
            int page = 1,
            int perPage = MoviesPerPageDefaultValue)
        {
            this.ViewData["CurrentSort"] = sortOrder;
            this.ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : string.Empty;
            this.ViewData["RatingSortParam"] = sortOrder == "rating" ? "ratingDesc" : "rating";

            if (searchString == null)
            {
                searchString = currentFilter;
            }

            this.ViewData["CurrentFilter"] = searchString;

            var movies = this.moviesService
                .GetAll<MovieViewModel>();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            movies = sortOrder switch
            {
                "rating" => movies.OrderBy(x => x.Rating).ToList(),
                "ratingDesc" => movies.OrderByDescending(x => x.Rating).ToList(),
                "nameDesc" => movies.OrderByDescending(x => x.Name).ToList(),
                _ => movies.OrderBy(x => x.Name).ToList(),
            };

            var pagesCount = (int)Math.Ceiling(movies.Count() / (decimal)perPage);

            movies = movies.Skip(perPage * (page - 1)).Take(perPage).ToList();

            var viewModel = new AllMoviesViewModel
            {
                Movies = movies.ToList(),
                CurrentPage = page,
                PagesCount = pagesCount,
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new MovieCreateInputModel
            {
                Actors = this.actorsService.GetAll<MovieActorsCreateViewModel>(),
                Genres = this.genresService.GetAll<MovieGenresCreateViewModel>(),
                Directors = this.directorsService.GetAll<MovieDirectorsCreateViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var actors = this.Request.Form["actorName"];
            var genres = this.Request.Form["genreName"];
            var directors = this.Request.Form["directorName"];

            var movie = await this.moviesService.CreaterMovieAsync(new CreateMovieServiceInputModel
            {
                Name = input.Name,
                Rating = input.Rating,
                IMDBLink = input.IMDBLink,
                TrailerLink = input.TrailerLink,
                Summary = input.Summary,
                Length = input.Length,
                Language = input.Language,
                ReleaseDate = input.ReleaseDate,
                PosterUrl = input.PosterUrl,
                Actors = actors,
                Genres = genres,
                Directors = directors,
            });

            this.TempData["CreateMovieTemp"] = "You have added a new movie to the database.";

            return this.RedirectToAction(nameof(this.Details), new { id = movie });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = this.moviesService.GetById<MovieEditViewModel>(id);

            var viewModel = new MovieEditViewModel
            {
                IMDBLink = movie.IMDBLink,
                Language = movie.Language,
                Length = movie.Length,
                TrailerLink = movie.TrailerLink,
                Name = movie.Name,
                Rating = movie.Rating,
                Summary = movie.Summary,
                ReleaseDate = movie.ReleaseDate,
                PosterUrl = movie.PosterUrl,
                Actors = this.actorsService.GetAll<MovieActorsEditInputModel>(),
                Genres = this.genresService.GetAll<MovieGenresEditInputModel>(),
                Directors = this.directorsService.GetAll<MovieDirectorEditInputModel>(),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditMovieInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var actors = this.Request.Form["actorName"];
            var genres = this.Request.Form["genreName"];
            var directors = this.Request.Form["directorName"];

            input.Actors = actors;
            input.Genres = genres;
            input.Directors = directors;

            await this.moviesService.EditActorAsync(input);

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.moviesService.DeleteMovieAsync(id);

            this.TempData["DeleteMovieTemp"] = "You have successfully deleted a movie from the database.";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> HardDelete(int id)
        {
            await this.moviesService.HardDeleteMovieAsync(id);

            this.TempData["HardDeleteMovieTemp"] = "You have successfully hard deleted a movie from the database.";

            return this.RedirectToAction(nameof(this.All));
        }

    }
}
