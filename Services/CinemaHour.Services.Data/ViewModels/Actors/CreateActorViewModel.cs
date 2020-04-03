namespace CinemaHour.Services.Data.ViewModels.Actors
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateActorViewModel
    {
        public string Image { get; set; }

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
