namespace CinemaHour.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;
    using CinemaHour.Data.Models.Enum;

    public class ActorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Actors.Any())
            {
                return;
            }

            var actors = new List<(string FirstName, string LastName, DateTime BirthDate, string Gender)>
            {
                ("Kevin", "Hart", DateTime.Parse("1979-06-07"), "Male"), // Added movie
                ("Dwayne", "Johnson", DateTime.Parse("1972-05-02"), "Male"), // Added movie
                ("Tom", "Hanks", DateTime.Parse("1956-07-09"), "Male"), // Added movie
                ("Al", "Pacino", DateTime.Parse("1940-04-25"), "Male"), // Added movie
                ("Denzel", "Washington", DateTime.Parse("1954-12-28"), "Male"), // Added movie
                ("Leonardo", "DiCaprio", DateTime.Parse("1974-11-11"), "Male"), // Added movie
                ("Johnny", "Depp", DateTime.Parse("1963-06-09"), "Male"), // Added movie
                ("Scarlet", "Johansson", DateTime.Parse("1984-11-22"), "Female"), // Added movie
            };

            foreach (var actor in actors)
            {
                dbContext.Actors.Add(new Actor
                {
                    FirstName = actor.FirstName,
                    LastName = actor.LastName,
                    BirthDate = actor.BirthDate,
                    Gender = Enum.Parse<Gender>(actor.Gender),
                });
            }
        }
    }
}
