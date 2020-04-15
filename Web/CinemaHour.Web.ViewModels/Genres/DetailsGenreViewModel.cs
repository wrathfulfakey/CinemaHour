namespace CinemaHour.Web.ViewModels.Genres
{
    using System.Collections.Generic;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class DetailsGenreViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<MovieDetailsGenreViewModel> Movies { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
