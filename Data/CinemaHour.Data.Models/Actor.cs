namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Common.Models;
    using CinemaHour.Data.Models.Enum;

    public class Actor : BaseDeletableModel<string>
    {
        public Actor()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Movies = new HashSet<MovieActors>();
        }

        //ToDo: add ActorImage
        //public string Image { get; set; }

        // Actor's First Name
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        // Birthdate of the actor
        [Required]
        public DateTime BirthDate { get; set; }

        // Movies the actor is starring
        public virtual ICollection<MovieActors> Movies { get; set; }
    }
}
