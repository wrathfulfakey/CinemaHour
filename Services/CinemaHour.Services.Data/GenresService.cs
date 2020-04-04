namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class GenresService : IGenresService
    {
        private readonly IDeletableEntityRepository<Genre> genreRepository;

        public GenresService(IDeletableEntityRepository<Genre> genreRepository)
        {
            this.genreRepository = genreRepository;
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
    }
}
