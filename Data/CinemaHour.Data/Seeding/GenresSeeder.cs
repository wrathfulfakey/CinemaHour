namespace CinemaHour.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;

    public class GenresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Genres.Any())
            {
                return;
            }

            var genres = new List<string>
            {
                "Action",
                "Adventure",
                "Biography",
                "Comedy",
                "Crime",
                "Drama",
                "Fantasy",
                "Historical",
                "Horror",
                "Mystery",
                "Romance",
                "Sci-Fi",
                "Thriller",
                "Western",
            };

            foreach (var genre in genres)
            {
                await dbContext.Genres.AddAsync(new Genre
                {
                    Name = genre,
                });
            }
        }
    }
}
