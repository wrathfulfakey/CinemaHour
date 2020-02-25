namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Replies = new HashSet<Comment>();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public ICollection<Comment> Replies { get; set; }
    }
}
