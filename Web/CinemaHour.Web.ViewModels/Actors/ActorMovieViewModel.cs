namespace CinemaHour.Web.ViewModels.Actors
{
    using System;

    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class ActorMovieViewModel : IMapFrom<MovieActors>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        //public byte[] Poster { get; set; }

        public float? Rating { get; set; }

        public string IMDBLink { get; set; }

        public string TrailerLink { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MovieActors, ActorMovieViewModel>().ForMember(
                   m => m.Name,
                   opt => opt.MapFrom(x => x.Movie.Name))
                .ForMember(
                    m => m.ReleaseDate,
                    opt => opt.MapFrom(x => x.Movie.ReleaseDate))
                .ForMember(
                    m => m.Rating,
                    opt => opt.MapFrom(x => x.Movie.Rating))
                .ForMember(
                    m => m.IMDBLink,
                    opt => opt.MapFrom(x => x.Movie.IMDBLink))
                .ForMember(
                    m => m.TrailerLink,
                    opt => opt.MapFrom(x => x.Movie.TrailerLink));
        }
    }
}
