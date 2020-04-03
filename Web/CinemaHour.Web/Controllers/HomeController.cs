namespace CinemaHour.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using CinemaHour.Services.Data;
    using CinemaHour.Web.CloudinaryHelper;
    using CinemaHour.Web.ViewModels;
    using CinemaHour.Web.ViewModels.Home;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IActorsService actorsService;
        private readonly IMoviesService moviesService;

        public HomeController(
            IActorsService actorsService,
            IMoviesService moviesService)
        {
            this.actorsService = actorsService;
            this.moviesService = moviesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Actors = this.actorsService.GetAll<IndexActorsViewModel>(3),
                Movies = this.moviesService.GetAll<IndexMoviesViewModel>(6),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        // Cloudinary
        // [HttpPost]
        // public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        // {
        //     var result = await CloudinaryExtension.UploadAsync(this.cloudinary, files);
        //     this.ViewBag.Links = result;
        //     return this.Redirect("/");
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
