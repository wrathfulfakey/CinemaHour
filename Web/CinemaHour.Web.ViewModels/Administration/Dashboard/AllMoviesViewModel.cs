namespace CinemaHour.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    public class AllMoviesViewModel
    {
        public IEnumerable<MoviesViewModel> Movies { get; set; }
    }
}
