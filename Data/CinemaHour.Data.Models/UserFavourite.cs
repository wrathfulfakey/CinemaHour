namespace CinemaHour.Data.Models
{
    public class UserFavourite
    {
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
