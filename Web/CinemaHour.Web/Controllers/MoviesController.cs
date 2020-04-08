namespace CinemaHour.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CinemaHour.Common;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Data.ViewModels.Movies;
    using CinemaHour.Web.ViewModels.Movies;
    using CinemaHour.Web.ViewModels.Movies.Edit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : Controller
    {
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

            return this.RedirectToAction(nameof(this.Details), new { id = id });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.moviesService.DeleteMovieAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> HardDelete(int id)
        {
            await this.moviesService.HardDeleteMovieAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
