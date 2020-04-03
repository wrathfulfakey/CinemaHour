namespace CinemaHour.Web.ViewModels.Home
{
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class IndexActorsViewModel : IMapFrom<Actor>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string FullName { get; set; }

        public string Info { get; set; }

        public string ShortInfo
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Info, @"<[^>]+>", string.Empty));

                return content.Length > 100
                    ? content.Substring(0, 100) + "..."
                    : content;
            }
        }

        public string Gender { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Actor, IndexActorsViewModel>()
                .ForMember(
                x => x.FullName,
                opt => opt.MapFrom(a => a.FirstName + " " + a.LastName))
                .ForMember(
                x => x.Gender,
                opt => opt.MapFrom(a => a.Gender.ToString()));
        }
    }
}
