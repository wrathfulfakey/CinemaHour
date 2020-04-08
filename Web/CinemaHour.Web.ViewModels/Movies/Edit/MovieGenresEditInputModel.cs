namespace CinemaHour.Web.ViewModels.Movies.Edit
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MovieGenresEditInputModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
