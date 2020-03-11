namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Common.Models;

    public class Genre : BaseModel<string>
    {
        public Genre()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Movies = new HashSet<MovieGenre>();
        }

        [Required]
        [StringLength(15, MinimumLength = 2)]
        public string Name { get; set; }

        public ICollection<MovieGenre> Movies { get; set; }
    }
}
