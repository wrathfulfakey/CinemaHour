namespace CinemaHour.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MovieCreateInputModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Movie name")]
        public string Name { get; set; }

        // Length of the movie in minutes
        [Required]
        [Range(30, 300)]
        public int Length { get; set; }

        // Release Date of the Movie
        [Display(Name = "Premiere Date")]
        public DateTime ReleaseDate { get; set; }

        // Movie Poster with default picture
        [Display(Name = "Movie Poster")]
        public string PosterUrl { get; set; }

        // Movie Language dub
        public string Language { get; set; }

        // Average Movie Rating
        [Range(0, 5)]
        public float? Rating { get; set; }

        [Display(Name= "IMDB Link")]
        public string IMDBLink { get; set; }

        [Display(Name= "Trailer Link")]
        public string TrailerLink { get; set; }

        public string Summary { get; set; }

        public virtual ICollection<MovieDirectorsCreateViewModel> Directors { get; set; }

        public virtual ICollection<MovieGenresCreateViewModel> Genres { get; set; }

        public virtual ICollection<MovieActorsCreateViewModel> Actors { get; set; }
    }
}
