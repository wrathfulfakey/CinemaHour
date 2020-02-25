namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Genre
    {
        public Genre()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Movies = new HashSet<MovieGenre>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<MovieGenre> Movies { get; set; }
    }
}
