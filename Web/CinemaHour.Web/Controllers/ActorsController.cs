namespace CinemaHour.Web.Controllers
{
    using System.Threading.Tasks;

    using CinemaHour.Common;
    using CinemaHour.Services.Data;
    using CinemaHour.Services.Data.ViewModels.Actors;
    using CinemaHour.Web.ViewModels.Actors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ActorsController : BaseController
    {
        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorsService)
        {
            this.actorsService = actorsService;
        }

        public IActionResult All()
        {
            var viewModel = new AllActorsViewModel
            {
                Actors = this.actorsService.GetAll<ActorViewModel>(),
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
    }
}
