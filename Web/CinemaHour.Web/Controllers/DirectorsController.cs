namespace CinemaHour.Web.Controllers
{
    using System.Threading.Tasks;

    using CinemaHour.Common;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels.Directors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DirectorsController : BaseController
    {
        private readonly IDirectorsService directorsService;

        public DirectorsController(IDirectorsService directorsService)
        {
            this.directorsService = directorsService;
        }

        public IActionResult All()
        {
            var viewModel = new AllDirectorsViewModel
            {
                Directors = this.directorsService.GetAll<DirectorViewModel>(),
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

            var directorId = await this.directorsService.EditDirectorAsync(id, input.Name);

            return this.RedirectToAction(nameof(this.Details), new { id = directorId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.directorsService.DeleteDirectorAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> HardDelete(string id)
        {
            await this.directorsService.HardDeleteDirectorAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
