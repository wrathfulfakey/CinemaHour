namespace CinemaHour.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;
    using Ganss.XSS;

    public class MovieDetailsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string GenreString { get; set; }

        public string PosterUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Length { get; set; }

        public string Language { get; set; }

        public string Summary { get; set; }

        public string SanitizedSummary => new HtmlSanitizer().Sanitize(this.Summary);

        public float? Rating { get; set; }

        public string IMDBLink { get; set; }

        public string TrailerLink { get; set; }

        public ICollection<GenreMovieViewModel> Genres { get; set; }

        public ICollection<DirectorsMovieViewModel> Directors { get; set; }

        public ICollection<ActorsMovieViewModel> Actors { get; set; }

        public ICollection<CommentsMovieViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieDetailsViewModel>()
                .ForMember(
                    x => x.GenreString,
                    opt => opt.MapFrom(m => string.Join(", ", m.Genres.Select(g => g.Genre.Name))));
        }
    }
}
