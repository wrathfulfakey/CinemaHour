namespace CinemaHour.Web.ViewModels.Movies
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class ActorsMovieViewModel : IMapFrom<MovieActors>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string ActorId { get; set; }

        public virtual Actor Actor { get; set; }
    }
}
