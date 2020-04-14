namespace CinemaHour.Web.ViewModels.Users
{
    using System;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class WatchedMoviesViewModel : IMapFrom<UserWatched>
    {
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public string MoviePosterUrl { get; set; }

        public DateTime MovieReleaseDate { get; set; }

        public float? MovieRating { get; set; }
    }
}
