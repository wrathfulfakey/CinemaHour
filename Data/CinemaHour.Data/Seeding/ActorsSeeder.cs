namespace CinemaHour.Data.Seeding
{
    using CinemaHour.Data.Models;
    using CinemaHour.Data.Models.Enum;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
                ("Kevin", "Hart", DateTime.Parse("1979-06-07"), "Male"),
                ("Tom", "Hanks", DateTime.Parse("1956-07-09"), "Male"),
                ("Al", "Pacino", DateTime.Parse("1940-04-25"), "Male"),
                ("Denzel", "Washington", DateTime.Parse("1954-12-28"), "Male"),
                ("Leonardo", "DiCaprio", DateTime.Parse("1974-11-11"), "Male"),
                ("Johnny", "Depp", DateTime.Parse("1963-06-09"), "Male"),
                ("Scarlet", "Johanson", DateTime.Parse("1984-11-22"), "Female"),
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
