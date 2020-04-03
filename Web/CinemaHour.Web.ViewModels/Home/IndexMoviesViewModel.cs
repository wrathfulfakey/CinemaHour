namespace CinemaHour.Web.ViewModels.Home
{
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    using System.Net;
    using System.Text.RegularExpressions;

    public class IndexMoviesViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string PosterUrl { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

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
    }
}
