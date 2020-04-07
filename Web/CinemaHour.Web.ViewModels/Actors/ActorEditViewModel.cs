﻿namespace CinemaHour.Web.ViewModels.Actors
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class ActorEditViewModel : IMapFrom<Actor>
    {
        public string Id { get; set; }

        [Display(Name = "Actor Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Information")]
        public string Info { get; set; }

        [Required]
        public string Gender { get; set; }

        // Birthdate of the actor
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }
    }
}
