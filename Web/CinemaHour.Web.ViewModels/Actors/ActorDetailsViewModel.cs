
namespace CinemaHour.Web.ViewModels.Actors
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class ActorDetailsViewModel : IMapFrom<Actor>, IHaveCustomMappings
    {
        public ActorDetailsViewModel()
        {
            this.Movies = new HashSet<ActorMovieViewModel>();
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age()
        {
            int age = DateTime.UtcNow.Year - this.BirthDate.Year;
            if ((this.BirthDate.Month > DateTime.UtcNow.Month) || (this.BirthDate.Month == DateTime.UtcNow.Month && this.BirthDate.Day > DateTime.UtcNow.Day))
            {
                age--;
            }

            return age;
        }

        public IEnumerable<ActorMovieViewModel> Movies { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Actor, ActorDetailsViewModel>().ForMember(
                m => m.Gender,
                opt => opt.MapFrom(x => x.Gender.ToString()));
        }
    }
}
