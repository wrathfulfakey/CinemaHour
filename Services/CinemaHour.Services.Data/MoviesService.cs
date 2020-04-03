namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public MoviesService(IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
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
    }
}
