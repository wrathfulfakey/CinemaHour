namespace CinemaHour.Web.ViewModels.Genres
{
    using System.ComponentModel.DataAnnotations;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class CreateGenreViewModel : IMapFrom<Genre>
    {
        [Required]
        [StringLength(15, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
