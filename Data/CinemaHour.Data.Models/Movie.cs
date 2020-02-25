namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Movie
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Actors = new List<MovieActors>();
            this.Comments = new List<MovieComment>();
            this.Genres = new List<MovieGenre>();
        }

        public string Id { get; set; }

        // Length of the movie in minutes
        public int Length { get; set; }

        // Release Date of the Movie
        public DateTime ReleaseDate { get; set; }

        // Movie Language dub
        public string Language { get; set; }


        // Average Movie Rating
        public float Rating { get; set; }

        public string DirectorId { get; set; }

        public MovieDirector Director { get; set; }

        public ICollection<MovieGenre> Genres { get; set; }

        public ICollection<MovieActors> Actors { get; set; }

        public ICollection<MovieComment> Comments { get; set; }
    }
}
