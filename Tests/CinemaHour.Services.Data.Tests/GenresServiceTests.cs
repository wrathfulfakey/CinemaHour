namespace CinemaHour.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using CinemaHour.Data;
    using CinemaHour.Data.Models;
    using CinemaHour.Data.Repositories;
    using CinemaHour.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class GenresServiceTests
    {
        public class GenreTestModel : IMapFrom<Genre>
        {
            public string Name { get; set; }
        }
    }
}
