namespace CinemaHour.Web.ViewModels.Movies
{

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class DirectorsMovieViewModel : IMapFrom<MovieDirector>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string DirectorId { get; set; }

        public virtual Director Director { get; set; }
    }
}
