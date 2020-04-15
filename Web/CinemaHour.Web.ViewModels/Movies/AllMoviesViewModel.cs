namespace CinemaHour.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    public class AllMoviesViewModel
    {
        public IEnumerable<MovieViewModel> Movies { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
