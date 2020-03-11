namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Common.Models;

    public class Director : BaseModel<string>
    {
        public Director()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Movies = new HashSet<MovieDirector>();
        }

        // First, Middle and Last name of director
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string FullName { get; set; }

        // Movies directed
        [Required]
        public ICollection<MovieDirector> Movies { get; set; }
    }
}
