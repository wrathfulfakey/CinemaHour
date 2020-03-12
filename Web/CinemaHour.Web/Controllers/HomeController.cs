namespace CinemaHour.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using CinemaHour.Web.CloudinaryHelper;
    using CinemaHour.Web.ViewModels;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly Cloudinary cloudinary;

        public HomeController(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
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

        public IActionResult Contact()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
