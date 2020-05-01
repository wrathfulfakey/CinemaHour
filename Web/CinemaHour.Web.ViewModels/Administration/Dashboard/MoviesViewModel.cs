namespace CinemaHour.Web.ViewModels.Administration.Dashboard
{
    using System;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MoviesViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string PosterUrl { get; set; }
    }
}
