namespace CinemaHour.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Data.ViewModels.Movies;
    using CinemaHour.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IRepository<MovieActors> actorsMovieRepository;
        private readonly IRepository<MovieGenre> genreMoviesRepository;
        private readonly IRepository<MovieDirector> directorMovieRepository;

        public MoviesService(
            IDeletableEntityRepository<Movie> moviesRepository,
            IRepository<MovieActors> actorsMovieRepository,
            IRepository<MovieGenre> genreMoviesRepository,
            IRepository<MovieDirector> directorMovieRepository)
        {
            this.moviesRepository = moviesRepository;
            this.actorsMovieRepository = actorsMovieRepository;
            this.genreMoviesRepository = genreMoviesRepository;
            this.directorMovieRepository = directorMovieRepository;
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

        public async Task RecoverMovie(int movieId)
        {
            var movie = this.moviesRepository
                .AllAsNoTrackingWithDeleted()
                .Where(x => x.Id == movieId && x.IsDeleted == true)
                .FirstOrDefault();

            if (movie == null)
            {
                return;
            }

            movie.IsDeleted = false;

            this.moviesRepository.Update(movie);
            await this.moviesRepository.SaveChangesAsync();
        }

        public ICollection<T> GetAllWithDeleted<T>()
        {
            IQueryable<Movie> query = this.moviesRepository
                .AllAsNoTrackingWithDeleted()
                .Where(x => x.IsDeleted)
                .OrderBy(x => x.DeletedOn);

            return query.To<T>().ToList();
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

            this.moviesRepository.HardDelete(movie);
            await this.moviesRepository.SaveChangesAsync();
        }
    }
}
