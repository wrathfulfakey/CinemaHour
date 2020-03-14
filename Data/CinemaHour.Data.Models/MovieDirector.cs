namespace CinemaHour.Data.Models
{
    public class MovieDirector
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string DirectorId { get; set; }

        public virtual Director Director { get; set; }
    }
}
