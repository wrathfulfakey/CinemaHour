namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Common.Models;

    public class Comment : BaseModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Replies = new HashSet<Comment>();
        }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Content { get; set; }

        public ICollection<Comment> Replies { get; set; }
    }
}
