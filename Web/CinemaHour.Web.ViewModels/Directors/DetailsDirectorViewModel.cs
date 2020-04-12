namespace CinemaHour.Web.ViewModels.Directors
{
    using System.Collections.Generic;

    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class DetailsDirectorViewModel : IMapFrom<Director>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public ICollection<MovieDetailsDirectorViewModel> Movies { get; set; }
    }
}
