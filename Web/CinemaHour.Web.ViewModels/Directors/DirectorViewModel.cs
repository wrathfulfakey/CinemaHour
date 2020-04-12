namespace CinemaHour.Web.ViewModels.Directors
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class DirectorViewModel : IMapFrom<Director>
    {
        public string Id { get; set; }

        public string FullName { get; set; }
    }
}
