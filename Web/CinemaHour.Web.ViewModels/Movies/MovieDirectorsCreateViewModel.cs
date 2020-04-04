namespace CinemaHour.Web.ViewModels.Movies
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MovieDirectorsCreateViewModel : IMapFrom<Director>
    {
        public string Id { get; set; }

        public string FullName { get; set; }
    }
}
