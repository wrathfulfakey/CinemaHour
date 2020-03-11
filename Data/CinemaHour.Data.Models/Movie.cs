﻿namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CinemaHour.Data.Common.Models;

    public class Movie : BaseModel<string>
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Actors = new HashSet<MovieActors>();
            this.Comments = new HashSet<MovieComment>();
            this.Genres = new HashSet<MovieGenre>();
            this.UsersFavourite = new HashSet<UserFavourite>();
            this.UsersWatched = new HashSet<UserWatched>();
        }

        // Length of the movie in minutes
        [Required]
        [Range(30, 300)]
        public int Length { get; set; }

        // Release Date of the Movie
        public DateTime? ReleaseDate { get; set; }

        // Movie Poster with default picture
        public byte[] Poster { get; set; }

        // Movie Language dub
        public string? Language { get; set; }

        // Average Movie Rating
        [Range(0, 5)]
        public float? Rating { get; set; }

        public string DirectorId { get; set; }

        public MovieDirector Director { get; set; }

        [Required]
        public ICollection<MovieGenre> Genres { get; set; }

        [Required]
        public ICollection<MovieActors> Actors { get; set; }

        public ICollection<MovieComment> Comments { get; set; }

        public virtual ICollection<UserFavourite> UsersFavourite { get; set; }

        public virtual ICollection<UserWatched> UsersWatched { get; set; }
    }
}
