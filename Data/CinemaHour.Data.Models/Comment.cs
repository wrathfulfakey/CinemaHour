namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

    }
}
