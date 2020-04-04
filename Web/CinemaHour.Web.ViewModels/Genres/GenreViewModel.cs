namespace CinemaHour.Web.ViewModels.Genres
{
    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class GenreViewModel : IMapFrom<Genre>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MoviesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Genre, GenreViewModel>()
                .ForMember(
                    x => x.MoviesCount,
                    z => z.MapFrom(g => g.Movies.Count));
        }
    }
}