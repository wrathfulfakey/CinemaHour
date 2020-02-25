namespace CinemaHour.Data.Models
{
    public class UserWatched
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public string MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
