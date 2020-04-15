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

    public class ActorsController : Controller
    {
        private const int ActorsPerPageDefaultValue = 12;
        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorsService)
        {
            this.actorsService = actorsService;
        }

        public IActionResult All(
            string searchString,
            string currentFilter,
            string sortOrder,
            int page = 1,
            int perPage = ActorsPerPageDefaultValue)
        {
            this.ViewData["CurrentSort"] = sortOrder;
            this.ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : string.Empty;

            if (searchString == null)
            {
                searchString = currentFilter;
            }

            this.ViewData["CurrentFilter"] = searchString;

            var actors = this.actorsService
               .GetAll<ActorViewModel>();

            if (!string.IsNullOrEmpty(searchString))
            {
                actors = actors.Where(x => x.FirstName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            actors = sortOrder switch
            {
                "nameDesc" => actors.OrderByDescending(x => x.FirstName).ToList(),
                _ => actors.OrderBy(x => x.FirstName).ToList(),
            };

            var pagesCount = (int)Math.Ceiling(this.actorsService.GetAll<ActorViewModel>().Count() / (decimal)perPage);

            actors = actors.Skip(perPage * (page - 1)).Take(perPage).ToList();

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

            this.TempData["CreateActorTemp"] = "You have added a new actor to the database.";

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

            this.TempData["DeleteActorTemp"] = "You have deleted an actor from the database.";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> HardDelete(string id)
        {
            await this.actorsService.HardDeleteActorAsync(id);

            this.TempData["HardDeleteActorTemp"] = "You have hard deleted an actor from the database.";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
