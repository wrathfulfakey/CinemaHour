namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public Comment()
        {
            this.Replies = new HashSet<Comment>();
        }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int MovieId { get; set; }

        public MovieComment Movie { get; set; }

        public virtual ICollection<Comment> Replies { get; set; }

    }
}
