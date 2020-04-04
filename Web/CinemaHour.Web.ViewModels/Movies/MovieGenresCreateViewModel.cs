namespace CinemaHour.Web.ViewModels.Movies
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MovieGenresCreateViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
