namespace CinemaHour.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Common;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Data.ViewModels.Actors;
    using CinemaHour.Web.ViewModels.Actors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ActorsController : BaseController
    {
        private const int ActorsPerPageDefaultValue = 12;
        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorsService)
        {
            this.actorsService = actorsService;
        }

        public IActionResult All(int page = 1, int perPage = ActorsPerPageDefaultValue)
        {
            var pagesCount = (int)Math.Ceiling(this.actorsService.GetAll<ActorViewModel>().Count() / (decimal)perPage);

            var actors = this.actorsService
               .GetAll<ActorViewModel>()
               .Skip(perPage * (page - 1))
               .Take(perPage);

            var viewModel = new AllActorsViewModel
            {
                Actors = actors,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
            var actorViewModel = this.actorsService
                .GetById<ActorDetailsViewModel>(id);

            if (actorViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(actorViewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(ActorCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var actor = await this.actorsService.CreateActorAsync(new CreateActorViewModel
            {
                Image = input.Image,
                Info = input.Info,
                FirstName = input.FirstName,
                LastName = input.LastName,
                BirthDate = input.BirthDate,
                Gender = input.Gender.ToString(),
            });

            return this.RedirectToAction(nameof(this.Details), new { id = actor });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var actor = this.actorsService
                .GetById<ActorEditViewModel>(id);

            if (actor == null)
            {
                return this.NotFound();
            }

            return this.View(actor);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditActorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.actorsService.EditActorAsync(input);

            return this.RedirectToAction(nameof(this.Details), new { id = input.Id });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.actorsService.DeleteActorAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> HardDelete(string id)
        {
            await this.actorsService.HardDeleteActorAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
