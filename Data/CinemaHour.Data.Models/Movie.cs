namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Common.Models;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.Actors = new HashSet<MovieActors>();
            this.Comments = new HashSet<MovieComment>();
            this.Genres = new HashSet<MovieGenre>();
            this.Directors = new HashSet<MovieDirector>();
            this.UsersFavourite = new HashSet<UserFavourite>();
            this.UsersWatched = new HashSet<UserWatched>();
        }

        // Movie Name
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        // Length of the movie in minutes
        [Required]
        [Range(30, 300)]
        public int Length { get; set; }

        // Release Date of the Movie
        public DateTime? ReleaseDate { get; set; }

        // Movie Poster with default picture
        public byte[] Poster { get; set; }

        // Movie Language dub
        public string Language { get; set; }

        // Average Movie Rating
        [Range(0, 5)]
        public float? Rating { get; set; }

        public string IMDBLink { get; set; }

        public string TrailerLink { get; set; }

        public string Summary { get; set; }

        public virtual ICollection<MovieDirector> Directors { get; set; }

        public virtual ICollection<MovieGenre> Genres { get; set; }

        public virtual ICollection<MovieActors> Actors { get; set; }

        public virtual ICollection<MovieComment> Comments { get; set; }

        public virtual ICollection<UserFavourite> UsersFavourite { get; set; }

        public virtual ICollection<UserWatched> UsersWatched { get; set; }
    }
}
