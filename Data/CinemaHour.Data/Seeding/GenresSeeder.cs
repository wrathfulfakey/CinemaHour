namespace CinemaHour.Data.Seeding
{
    using CinemaHour.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
                "Comedy",
                "Sci-Fi",
                "Action",
                "Drama",
                "Historical",
                "Adventure",
                "Crime",
                "Fantasy",
                "Horror",
                "Romance",
                "Western",
                "Thriller",
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
