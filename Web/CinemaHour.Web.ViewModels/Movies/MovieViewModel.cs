namespace CinemaHour.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MovieViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PosterUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Length { get; set; }

        public string Summary { get; set; }

        public ICollection<GenreMovieViewModel> Genres { get; set; }

        public string GenreString { get; set; }

        public string ShortSummary
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Summary, @"<[^>]+>", string.Empty));

                return content.Length > 100
                    ? content.Substring(0, 100) + "..."
                    : content;
            }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieViewModel>()
                .ForMember(
                    x => x.GenreString,
                    opt => opt.MapFrom(m => string.Join(", ", m.Genres.Select(g => g.Genre.Name))));
        }
    }
}
