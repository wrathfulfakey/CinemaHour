namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Mapping;

    public class GenresService : IGenresService
    {
        private readonly IDeletableEntityRepository<Genre> genreRepository;
        private readonly IRepository<MovieGenre> movieGenreRepository;

        public GenresService(
            IDeletableEntityRepository<Genre> genreRepository,
            IRepository<MovieGenre> movieGenreRepository)
        {
            this.genreRepository = genreRepository;
            this.movieGenreRepository = movieGenreRepository;
        }

        public ICollection<T> GetAll<T>(int? count = null)
        {
            IQueryable<Genre> query = this.genreRepository.All()
                .OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public ICollection<T> GetAllMovies<T>(int id)
        {
            IQueryable<Movie> query = this.movieGenreRepository.All()
                .Where(g => g.GenreId == id)
                .Select(m => m.Movie);

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var genre = this.genreRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return genre;
        }
    }
}
