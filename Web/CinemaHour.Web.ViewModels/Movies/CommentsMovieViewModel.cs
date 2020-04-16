namespace CinemaHour.Web.ViewModels.Movies
{
    using System;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;
    using Ganss.XSS;

    public class CommentsMovieViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public string UserAvatarUrl { get; set; }
    }
}
