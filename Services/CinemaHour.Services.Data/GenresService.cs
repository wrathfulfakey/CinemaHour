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
        private readonly IRepository<MovieGenre> movieGenresRepository;

        public GenresService(
            IDeletableEntityRepository<Genre> genreRepository,
            IRepository<MovieGenre> movieGenresRepository)
        {
            this.genreRepository = genreRepository;
            this.movieGenresRepository = movieGenresRepository;
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

        public ICollection<T> GetAllMovies<T>(int genreId)
        {
            IQueryable<MovieGenre> query = this.movieGenresRepository.All()
                .Where(x => x.GenreId == genreId)
                .OrderBy(x => x.Movie.Name);

            return query.To<T>().ToList();
        }
    }
}
