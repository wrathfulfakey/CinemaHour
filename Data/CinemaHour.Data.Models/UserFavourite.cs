namespace CinemaHour.Data.Models
{
    public class UserFavourite
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public string MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
