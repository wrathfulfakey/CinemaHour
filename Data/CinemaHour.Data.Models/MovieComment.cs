namespace CinemaHour.Data.Models
{
    public class MovieComment
    {
        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public string CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
