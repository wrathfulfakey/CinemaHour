namespace CinemaHour.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Mapping;

    public class DirectorsService : IDirectorsService
    {
        private readonly IDeletableEntityRepository<Director> directorsRepository;
        private readonly IRepository<MovieDirector> movieDirectorsRepository;

        public DirectorsService(
            IDeletableEntityRepository<Director> directorsRepository,
            IRepository<MovieDirector> movieDirectorsRepository)
        {
            this.directorsRepository = directorsRepository;
            this.movieDirectorsRepository = movieDirectorsRepository;
        }

        public ICollection<T> GetAll<T>(int? count = null)
        {
            IQueryable<Director> query = this.directorsRepository.All()
                .OrderBy(x => x.FullName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<string> CreateDirectorAsync(string fullName)
        {
            var director = new Director
            {
                FullName = fullName,
            };

            if (this.directorsRepository.All().Any(x => x.FullName == fullName))
            {
                return "There is already a director with the same name.";
            }

            await this.directorsRepository.AddAsync(director);
            await this.directorsRepository.SaveChangesAsync();

            return director.Id;
        }

        public async Task DeleteDirectorAsync(string id)
        {
            var director = await this.directorsRepository.GetByIdWithDeletedAsync(id);

            this.directorsRepository.Delete(director);
            await this.directorsRepository.SaveChangesAsync();
        }

        public async Task HardDeleteDirectorAsync(string id)
        {
            var director = await this.directorsRepository.GetByIdWithDeletedAsync(id);

            var directorMoviesToDelete = this.movieDirectorsRepository.All()
                .Where(x => x.DirectorId == id);
            foreach (var movie in directorMoviesToDelete)
            {
                this.movieDirectorsRepository.Delete(movie);
            }

            this.directorsRepository.HardDelete(director);
            await this.directorsRepository.SaveChangesAsync();
        }

        public T GetById<T>(string id)
        {
            var director = this.directorsRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return director;
        }

        public ICollection<T> GetAllMovies<T>(string id)
        {
            IQueryable<Movie> query = this.movieDirectorsRepository.All()
                .Where(g => g.DirectorId == id)
                .Select(m => m.Movie);

            return query.To<T>().ToList();
        }

        public async Task<string> EditDirectorAsync(string id, string directorFullName)
        {
            var director = await this.directorsRepository.GetByIdWithDeletedAsync(id);

            director.FullName = directorFullName;

            this.directorsRepository.Update(director);
            await this.directorsRepository.SaveChangesAsync();

            return director.Id;
        }
    }
}
