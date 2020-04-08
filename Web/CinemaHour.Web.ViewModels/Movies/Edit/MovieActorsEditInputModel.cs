namespace CinemaHour.Web.ViewModels.Movies.Edit
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MovieActorsEditInputModel : IMapFrom<Actor>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
