namespace CinemaHour.Web.ViewModels.Actors
{
    using System;
    using System.Globalization;

    using AutoMapper;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class ActorViewModel : IMapFrom<Actor>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Actor, ActorViewModel>().ForMember(
                m => m.Gender,
                opt => opt.MapFrom(x => x.Gender.ToString()));
        }
    }
}
