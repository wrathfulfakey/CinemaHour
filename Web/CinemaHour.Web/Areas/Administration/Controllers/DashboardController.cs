namespace CinemaHour.Web.Areas.Administration.Controllers
{
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                SettingsCount = this.settingsService.GetCount(),
            };
            return this.View(viewModel);
        }
    }
}
