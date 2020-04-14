namespace CinemaHour.Web.ViewModels.Users
{
    using System.Collections.Generic;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string AvatarUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TotalTimeWatched { get; set; }

        public ICollection<FavouriteMoviesViewModel> Favourites { get; set; }

        public ICollection<WatchedMoviesViewModel> Watched { get; set; }
    }
}
