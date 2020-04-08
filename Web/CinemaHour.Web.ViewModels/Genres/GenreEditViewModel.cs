namespace CinemaHour.Web.ViewModels.Genres
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class GenreEditViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
