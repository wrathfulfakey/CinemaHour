namespace CinemaHour.Web.ViewModels.Movies.Edit
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MovieDirectorEditInputModel : IMapFrom<Director>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

    }
}
