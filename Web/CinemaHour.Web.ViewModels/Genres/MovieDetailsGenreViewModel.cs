namespace CinemaHour.Web.ViewModels.Genres
{
    using System.Net;
    using System.Text.RegularExpressions;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MovieDetailsGenreViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string PosterUrl { get; set; }

        public string Name { get; set; }

        public float Rating { get; set; }

        public string Summary { get; set; }

        public string ShortSummary
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Summary, @"<[^>]+>", string.Empty));

                return content.Length > 75
                    ? content.Substring(0, 75) + "..."
                    : content;
            }
        }
    }
}