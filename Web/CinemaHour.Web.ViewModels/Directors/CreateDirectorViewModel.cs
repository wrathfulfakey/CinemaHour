namespace CinemaHour.Web.ViewModels.Directors
{
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class CreateDirectorViewModel : IMapFrom<Director>
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string FullName { get; set; }
    }
}
