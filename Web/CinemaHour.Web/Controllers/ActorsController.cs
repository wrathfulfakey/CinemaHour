namespace CinemaHour.Web.Controllers
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data;
    using CinemaHour.Services.Mapping;
    using CinemaHour.Web.ViewModels.Actors;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

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
            var viewModel = this.actorsService
                .GetAll<ActorDetailsViewModel>()
                .FirstOrDefault(x => x.Id == id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
