namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class ActorsService : IActorsService
    {
        private readonly IDeletableEntityRepository<Actor> actorsRepository;

        public ActorsService(IDeletableEntityRepository<Actor> actorsRepository)
        {
            this.actorsRepository = actorsRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Actor> query = this.actorsRepository.All()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        // Admins can [create], [delete] AND [edit] (same with directors, movies, comments, users) new actors (movies can be added AFTER we add actors and directors)
        // public void Create() { }

        public Actor GetById(string id)
            => this.actorsRepository.All().FirstOrDefault(x => x.Id == id);
    }
}
