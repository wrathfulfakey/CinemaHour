namespace CinemaHour.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Common;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels.Directors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DirectorsController : BaseController
    {
        private const int DirectorsPerPageDefaultValue = 12;
        private readonly IDirectorsService directorsService;

        public DirectorsController(IDirectorsService directorsService)
        {
            this.directorsService = directorsService;
        }

        public IActionResult All(int page = 1, int perPage = DirectorsPerPageDefaultValue)
        {
            var pagesCount = (int)Math.Ceiling(this.directorsService.GetAll<DirectorViewModel>().Count() / (decimal)perPage);

            var directors = this.directorsService
               .GetAll<DirectorViewModel>()
               .Skip(perPage * (page - 1))
               .Take(perPage);

            var viewModel = new AllDirectorsViewModel
            {
                Directors = directors,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
            var director = this.directorsService.GetById<DirectorViewModel>(id);

            var viewModel = new DetailsDirectorViewModel
            {
                Id = director.Id,
                FullName = director.FullName,
                Movies = this.directorsService.GetAllMovies<MovieDetailsDirectorViewModel>(id),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateDirectorViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.directorsService.CreateDirectorAsync(input.FullName);

            this.TempData["CreateDirectorTemp"] = "You have added a new director to the database.";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var director = this.directorsService
                .GetById<EditDirectorViewModel>(id);

            if (director == null)
            {
                return this.NotFound();
            }

            return this.View(director);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditDirectorViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var directorId = await this.directorsService.EditDirectorAsync(id, input.FullName);

            return this.RedirectToAction(nameof(this.Details), new { id = directorId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.directorsService.DeleteDirectorAsync(id);

            this.TempData["DeleteDirectorTemp"] = "You have deleted a director from the database.";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> HardDelete(string id)
        {
            await this.directorsService.HardDeleteDirectorAsync(id);

            this.TempData["HardDeleteDirectorTemp"] = "You have hard deleted a director from the database.";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
