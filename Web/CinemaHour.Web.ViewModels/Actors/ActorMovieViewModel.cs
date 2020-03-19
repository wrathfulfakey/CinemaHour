namespace CinemaHour.Web.ViewModels.Actors
{
    using System;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class ActorMovieViewModel : IMapFrom<Movie>
    {
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public byte[] Poster { get; set; }

        public float? Rating { get; set; }

        public string IMDBLink { get; set; }

        public string TrailerLink { get; set; }
    }
}
