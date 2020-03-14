namespace CinemaHour.Data.Models
{
    public class MovieActors
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string ActorId { get; set; }

        public virtual Actor Actor { get; set; }
    }
}
