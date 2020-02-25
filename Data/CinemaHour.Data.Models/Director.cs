namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Director
    {
        public Director()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Movies = new List<MovieDirector>();
        }

        public string Id { get; set; }

        // First, Middle and Last name of director
        public string FullName { get; set; }

        // Movies directed
        public ICollection<MovieDirector> Movies { get; set; }
    }
}
