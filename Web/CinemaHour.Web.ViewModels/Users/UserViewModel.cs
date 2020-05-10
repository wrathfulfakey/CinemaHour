namespace CinemaHour.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public string AvatarUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TotalTimeWatched { get; set; }

        public ICollection<FavouriteMoviesViewModel> Favourites { get; set; }

        public ICollection<WatchedMoviesViewModel> Watched { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(
                    x => x.TotalTimeWatched,
                    y => y.MapFrom(u => u.Watched
                    .Where(m => !m.Movie.IsDeleted)
                    .Sum(w => w.Movie.Length)));
        }
    }
}
