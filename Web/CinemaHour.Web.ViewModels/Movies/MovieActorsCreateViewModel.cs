namespace CinemaHour.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MovieActorsCreateViewModel : IMapFrom<Actor>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
