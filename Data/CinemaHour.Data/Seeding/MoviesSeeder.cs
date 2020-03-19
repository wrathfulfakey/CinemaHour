namespace CinemaHour.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;

    public class MoviesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Movies.Any())
            {
                return;
            }

            var movies = new List<(string Name, int Length, DateTime ReleaseDate, string Language, float Rating, string IMDBLink, string TrailerLink, string Summary, string[] Genres, string[] Actors, string[] Directors)>
            {
                ("Central Intelligence", 107, DateTime.Parse("2016-06-10"), "English", 3.5f, "https://www.imdb.com/title/tt1489889/", "https://www.youtube.com/watch?v=MxEw3elSJ8M", "After he reconnects with an awkward pal from high school through Facebook, a mild-mannered accountant is lured into the world of international espionage.", new[] {"Comedy", "Action", "Crime" }, new[]
                { "Kevin Hart", "Dwayne Johnson" }, new[] { "Rawson Marshall Thurber" }),
                ("Avengers: Infinity War", 159, DateTime.Parse("2018-04-23"), "English", 4.7f, "https://www.imdb.com/title/tt4154756", "https://www.youtube.com/watch?v=6ZfuNTqbHE8", "The Avengers and their allies must be willing to sacrifice all in an attempt to defeat the powerful Thanos before his blitz of devastation and ruin puts an end to the universe.", new[] { "Action", "Adventure", "Sci-Fi"}, new[] { "Scarlet Johansson" }, new[] { "Joe Russo", "Anthony Russo" }),
                ("Scarface", 170, DateTime.Parse("1983-12-09"), "English", 4.4f, "https://www.imdb.com/title/tt0086250", "https://www.youtube.com/watch?v=7pQQHnqBa2E", "In 1980 Miami, a determined Cuban immigrant takes over a drug cartel and succumbs to greed.", new[] { "Crime", "Drama" }, new[] { "Al Pacino" }, new[] { "Brian De Palma" } ),
                ("Forrest Gump", 144, DateTime.Parse("1994-07-06"), "English", 4.6f, "https://www.imdb.com/title/tt0109830", "https://www.youtube.com/watch?v=bLvqoHBptjg", "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold through the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.", new[] { "Drama", "Romance" }, new[] { "Tom Hanks" }, new[] { "Robert Zemeckis" }),
                ("Training Day", 122, DateTime.Parse("2001-10-05"), "English", 3.9f, "https://www.imdb.com/title/tt0139654/", "https://www.youtube.com/watch?v=DXPJqRtkDP0", "On his first day on the job as a Los Angeles narcotics officer, a rookie cop goes beyond a full work day in training within the narcotics division of the L.A.P.D. with a rogue detective who isn't what he appears to be.", new[] { "Crime", "Drama", "Thriller" }, new[] { "Denzel Washington" }, new[] { "Antoine Fuqua" }),
                ("The Wolf of Wall Street", 180, DateTime.Parse("2013-12-25"), "English", 4.1f, "https://www.imdb.com/title/tt0993846", "https://www.youtube.com/watch?v=iszwuX1AK6A", "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.", new[] { "Biography", "Crime", "Drama" }, new[] { "Leonardo DiCaprio" }, new[] { "Martin Scorsese" }),
                ("Murder on the Orient Express", 114, DateTime.Parse("2017-11-03"), "English", 3.2f, "https://www.imdb.com/title/tt3402236", "https://www.youtube.com/watch?v=Mq4m3yAoW8E", "When a murder occurs on the train on which he's travelling, celebrated detective Hercule Poirot is recruited to solve the case.", new[] { "Crime", "Drama", "Mystery" }, new[] { "Johnny Depp" }, new[] { "Kenneth Branagh" }),
            };

            foreach (var movie in movies)
            {
                var movieToAdd = new Movie
                {
                    Name = movie.Name,
                    Length = movie.Length,
                    ReleaseDate = movie.ReleaseDate,
                    Language = movie.Language,
                    Rating = movie.Rating,
                    IMDBLink = movie.IMDBLink,
                    TrailerLink = movie.TrailerLink,
                    Summary = movie.Summary,
                };

                // Seed genres in movie
                var genres = movie.Genres;
                foreach (var genre in genres)
                {
                    movieToAdd.Genres.Add(new MovieGenre
                    {
                        GenreId = dbContext.Genres.Where(x => x.Name == genre).FirstOrDefault().Id,
                        MovieId = movieToAdd.Id,
                    });
                }

                // Seed actors in movie
                var actors = movie.Actors;
                foreach (var actor in actors)
                {
                    movieToAdd.Actors.Add(new MovieActors
                    {
                        ActorId = dbContext.Actors.Where(x => (x.FirstName + " " + x.LastName) == actor).FirstOrDefault().Id,
                        MovieId = movieToAdd.Id,
                    });
                }

                // Seed directors in movie
                var directors = movie.Directors;
                foreach (var director in directors)
                {
                    movieToAdd.Directors.Add(new MovieDirector
                    {
                        DirectorId = dbContext.Directors.Where(x => x.FullName == director).FirstOrDefault().Id,
                        MovieId = movieToAdd.Id,
                    });
                }

                dbContext.Movies.Add(movieToAdd);
            }
        }
    }
}
