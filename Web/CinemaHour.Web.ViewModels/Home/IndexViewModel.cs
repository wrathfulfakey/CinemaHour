namespace CinemaHour.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexActorsViewModel> Actors { get; set; }

        public IEnumerable<IndexMoviesViewModel> Movies { get; set; }
    }
}
