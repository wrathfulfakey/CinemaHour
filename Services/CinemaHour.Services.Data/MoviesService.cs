namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Data.ViewModels.Movies;
    using CinemaHour.Services.Mapping;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IRepository<MovieActors> actorsMovieRepository;
        private readonly IRepository<MovieGenre> genreMoviesRepository;
        private readonly IRepository<MovieDirector> directorMovieRepository;
        private readonly IRepository<MovieComment> commentsMoviesRepository;

        public MoviesService(
            IDeletableEntityRepository<Movie> moviesRepository,
            IRepository<MovieActors> actorsMovieRepository,
            IRepository<MovieGenre> genreMoviesRepository,
            IRepository<MovieDirector> directorMovieRepository,
            IRepository<MovieComment> commentsMoviesRepository)
        {
            this.moviesRepository = moviesRepository;
            this.actorsMovieRepository = actorsMovieRepository;
            this.genreMoviesRepository = genreMoviesRepository;
            this.directorMovieRepository = directorMovieRepository;
            this.commentsMoviesRepository = commentsMoviesRepository;
        }

        public async Task<int> CreaterMovieAsync(CreateMovieServiceInputModel input)
        {
            var movie = new Movie
            {
                IMDBLink = input.IMDBLink,
                TrailerLink = input.IMDBLink,
                Language = input.Language,
                Length = input.Length,
                Name = input.Name,
                PosterUrl = input.PosterUrl,
                Rating = input.Rating,
                ReleaseDate = input.ReleaseDate,
                Summary = input.Summary,
            };

            // Add actors
            var actors = input.Actors;
            foreach (var actor in actors)
            {
                movie.Actors.Add(new MovieActors
                {
                    ActorId = actor,
                    MovieId = movie.Id,
                });
            }

            // Add genres
            var genres = input.Genres;
            foreach (var genre in genres)
            {
                var intGenre = int.Parse(genre);

                movie.Genres.Add(new MovieGenre
                {
                    GenreId = intGenre,
                    MovieId = movie.Id,
                });
            }

            // Add directors
            var directors = input.Directors;
            foreach (var director in directors)
            {
                movie.Directors.Add(new MovieDirector
                {
                    DirectorId = director,
                    MovieId = movie.Id,
                });
            }

            await this.moviesRepository.AddAsync(movie);
            await this.moviesRepository.SaveChangesAsync();

            return movie.Id;
        }


        public ICollection<T> GetAll<T>(int? count = null)
        {
            IQueryable<Movie> query = this.moviesRepository.All()
                .OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var movie = this.moviesRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return movie;
        }

        public async Task DeleteActorAsync(int id)
        {
            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(id);

            this.moviesRepository.Delete(movie);
            await this.moviesRepository.SaveChangesAsync();
        }

        public async Task HardDeleteActorAsync(int id)
        {
            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(id);
            var actorMoviesToDelete = this.actorsMovieRepository.All().Where(x => x.MovieId == id);
            foreach (var actorMovie in actorMoviesToDelete)
            {
                this.actorsMovieRepository.Delete(actorMovie);
            }

            var genreMoviesToDelete = this.genreMoviesRepository.All().Where(x => x.MovieId == id);
            foreach (var genreMovie in genreMoviesToDelete)
            {
                this.genreMoviesRepository.Delete(genreMovie);
            }

            var directorMoviesToDelete = this.directorMovieRepository.All().Where(x => x.MovieId == id);
            foreach (var directorMovie in directorMoviesToDelete)
            {
                this.directorMovieRepository.Delete(directorMovie);
            }

            var commentsMoviesToDelete = this.commentsMoviesRepository.All().Where(x => x.MovieId == id);
            foreach (var commentsMovie in commentsMoviesToDelete)
            {
                this.commentsMoviesRepository.Delete(commentsMovie);
            }

            this.moviesRepository.HardDelete(movie);
            await this.moviesRepository.SaveChangesAsync();
        }
    }
}
