namespace CinemaHour.Web.Controllers
{
    using CinemaHour.Services.Data;
    using CinemaHour.Web.ViewModels.Actors;
    using Microsoft.AspNetCore.Mvc;

    public class ActorsController : Controller
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
            var viewModel = new ActorDetailsViewModel();

            return this.View($"/Actors/Details?id={id}", viewModel);
        }
    }
}
