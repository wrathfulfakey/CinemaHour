namespace CinemaHour.Data.Models
{
    public class MovieDirector
    {
        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public string DirectorId { get; set; }

        public Director Director { get; set; }
    }
}
