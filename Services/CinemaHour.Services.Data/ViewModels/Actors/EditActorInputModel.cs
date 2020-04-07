namespace CinemaHour.Services.Data.ViewModels.Actors
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class EditActorInputModel : IMapFrom<Actor>
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }

        public string Info { get; set; }

        [Required]
        public string Gender { get; set; }

        // Birthdate of the actor
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
