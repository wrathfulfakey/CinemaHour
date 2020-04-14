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
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IRepository<MovieActors> actorsMovieRepository;
        private readonly IRepository<MovieGenre> genreMoviesRepository;
        private readonly IRepository<MovieDirector> directorMovieRepository;
        private readonly IRepository<MovieComment> commentsMoviesRepository;
        private readonly IRepository<UserFavourite> usersFavouriteRepository;
        private readonly IRepository<UserWatched> usersWatchedRepository;

        public MoviesService(
            IDeletableEntityRepository<Movie> moviesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IRepository<MovieActors> actorsMovieRepository,
            IRepository<MovieGenre> genreMoviesRepository,
            IRepository<MovieDirector> directorMovieRepository,
            IRepository<MovieComment> commentsMoviesRepository,
            IRepository<UserFavourite> usersFavouriteRepository,
            IRepository<UserWatched> usersWatchedRepository)
        {
            this.moviesRepository = moviesRepository;
            this.usersRepository = usersRepository;
            this.actorsMovieRepository = actorsMovieRepository;
            this.genreMoviesRepository = genreMoviesRepository;
            this.directorMovieRepository = directorMovieRepository;
            this.commentsMoviesRepository = commentsMoviesRepository;
            this.usersFavouriteRepository = usersFavouriteRepository;
            this.usersWatchedRepository = usersWatchedRepository;
        }

        public async Task<string> AddMovieToWatched(int movieId, string username)
        {
            var user = this.usersRepository.All()
                .Where(x => x.UserName == username)
                .FirstOrDefault();

            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);

            var checkIfExists = this.usersWatchedRepository.All()
                .Where(x => x.ApplicationUserId == user.Id && x.MovieId == movieId)
                .FirstOrDefault();

            user.Watched = this.usersWatchedRepository.All().Where(x => x.ApplicationUserId == user.Id).ToList();

            if (checkIfExists != null)
            {
                return $"You already have {movie.Name} in your watched list.";
            }

            var userWatched = new UserWatched
            {
                ApplicationUserId = user.Id,
                MovieId = movieId,
            };

            user.Watched.Add(userWatched);

            user.TotalTimeWatched += movie.Length;

            await this.usersRepository.SaveChangesAsync();

            return $"You have successfully added {movie.Name} to your watched list.";
        }

        public async Task<string> AddMovieToFavourites(int movieId, string username)
        {
            var user = this.usersRepository.All()
                .Where(x => x.UserName == username)
                .FirstOrDefault();

            var checkIfExists = this.usersFavouriteRepository.All()
                .Where(x => x.ApplicationUserId == user.Id && x.MovieId == movieId)
                .FirstOrDefault();

            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(movieId);

            if (checkIfExists != null)
            {
                return $"You already have {movie.Name} in your favourites list.";
            }

            var userFavourite = new UserFavourite
            {
                ApplicationUserId = user.Id,
                MovieId = movieId,
            };

            user.Favourites.Add(userFavourite);

            await this.usersRepository.SaveChangesAsync();

            return $"You have successfully added {movie.Name} to your favourites list.";
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

            if (this.moviesRepository.All().Any(x => x.Name == input.Name))
            {
                return 0;
            }

            await this.moviesRepository.AddAsync(movie);
            await this.moviesRepository.SaveChangesAsync();

            return movie.Id;
        }

        public async Task EditActorAsync(EditMovieInputModel input)
        {
            var entity = this.moviesRepository.All()
                .Where(x => x.Id == input.Id)
                .FirstOrDefault();

            entity.Actors = this.actorsMovieRepository.All()
                .Where(x => x.MovieId == input.Id)
                .ToList();
            entity.Directors = this.directorMovieRepository.All()
                .Where(x => x.MovieId == input.Id)
                .ToList();
            entity.Genres = this.genreMoviesRepository.All()
                .Where(x => x.MovieId == input.Id)
                .ToList();

            ICollection<MovieActors> inputActors = new HashSet<MovieActors>();
            foreach (var actor in input.Actors)
            {
                var actorMovie = new MovieActors
                {
                    ActorId = actor,
                    MovieId = entity.Id,
                };

                if (entity.Actors.Contains(actorMovie))
                {
                    continue;
                }

                if (!input.Actors.Contains(actorMovie.ActorId))
                {
                    entity.Actors.Remove(actorMovie);
                }

                inputActors.Add(actorMovie);
            }

            ICollection<MovieGenre> inputGenres = new HashSet<MovieGenre>();
            foreach (var genre in input.Genres)
            {
                var parsedGenreId = int.Parse(genre);
                var genreMovie = new MovieGenre
                {
                    GenreId = parsedGenreId,
                    MovieId = entity.Id,
                };

                if (entity.Genres.Contains(genreMovie))
                {
                    continue;
                }

                if (!input.Genres.Contains(genreMovie.GenreId.ToString()))
                {
                    entity.Genres.Remove(genreMovie);
                }

                inputGenres.Add(genreMovie);
            }

            ICollection<MovieDirector> inputDirectors = new HashSet<MovieDirector>();
            foreach (var director in input.Directors)
            {
                var directorMovie = new MovieDirector
                {
                    DirectorId = director,
                    MovieId = entity.Id,
                };

                if (entity.Directors.Contains(directorMovie))
                {
                    continue;
                }

                if (!input.Directors.Contains(directorMovie.DirectorId))
                {
                    entity.Directors.Remove(directorMovie);
                }

                inputDirectors.Add(directorMovie);
            }

            entity.Actors = inputActors;
            entity.Genres = inputGenres;
            entity.Directors = inputDirectors;

            entity.IMDBLink = input.IMDBLink;
            entity.Language = input.Language;
            entity.Name = input.Name;
            entity.PosterUrl = input.PosterUrl;
            entity.Rating = input.Rating;
            entity.ReleaseDate = input.ReleaseDate;
            entity.TrailerLink = input.TrailerLink;
            entity.Summary = input.Summary;

            this.moviesRepository.Update(entity);
            await this.moviesRepository.SaveChangesAsync();
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

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await this.moviesRepository.GetByIdWithDeletedAsync(id);

            this.moviesRepository.Delete(movie);
            await this.moviesRepository.SaveChangesAsync();
        }

        public async Task HardDeleteMovieAsync(int id)
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
