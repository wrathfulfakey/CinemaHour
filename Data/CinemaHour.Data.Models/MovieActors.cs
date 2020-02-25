namespace CinemaHour.Data.Models
{
    public class MovieActors
    {
        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public string ActorId { get; set; }

        public Actor Actor { get; set; }
    }
}
