namespace CinemaHour.Services.Data.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class CreateMovieServiceInputModel : IMapFrom<Movie>
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        // Length of the movie in minutes
        [Required]
        [Range(30, 300)]
        public int Length { get; set; }

        // Release Date of the Movie
        public DateTime ReleaseDate { get; set; }

        // Movie Poster with default picture
        public string PosterUrl { get; set; }

        // Movie Language dub
        public string Language { get; set; }

        // Average Movie Rating
        [Range(0, 5)]
        public float? Rating { get; set; }

        public string IMDBLink { get; set; }

        public string TrailerLink { get; set; }

        public string Summary { get; set; }

        public virtual ICollection<string> Directors { get; set; }

        public virtual ICollection<string> Genres { get; set; }

        public virtual ICollection<string> Actors { get; set; }
    }
}
