namespace CinemaHour.Web.ViewModels.Administration.Dashboard
{
    using System;

    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class NewUserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, NewUserViewModel>()
                .ForMember(
                x => x.FullName,
                y => y.MapFrom(u => u.FirstName + " " + u.LastName));
        }
    }
}