namespace CinemaHour.Data.Seeding
{
    using CinemaHour.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DirectorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Directors.Any())
            {
                return;
            }

            var directors = new List<string>
            {
                "Antoine Fuqua",
                "Rawson Marshall Thurber",
                "Anthony Russo",
                "Joe Russo",
                "Brian De Palma",
                "Robert Zemeckis",
                "Martin Scorsese",
                "Kenneth Branagh",
            };

            foreach (var director in directors)
            {
                dbContext.Directors.Add(new Director
                {
                    FullName = director,
                });
            }
        }
    }
}
