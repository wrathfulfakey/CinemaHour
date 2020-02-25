namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CinemaHour.Data.Models.Enum;

    public class Actor
    {
        public Actor()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Movies = new HashSet<MovieActors>();
        }

        public string Id { get; set; }

        // Actor's First Name
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        // Birthdate of the actor
        public DateTime? BirthDate { get; set; }

        // Movies the actor is starring
        public ICollection<MovieActors> Movies { get; set; }
    }
}
