namespace CinemaHour.Web.ViewModels.Movies
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class GenreMovieViewModel : IMapFrom<MovieGenre>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
    }
}