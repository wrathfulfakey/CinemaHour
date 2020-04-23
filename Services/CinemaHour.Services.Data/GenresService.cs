namespace CinemaHour.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Mapping;
    using Xunit.Sdk;

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

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await this.genreRepository.GetByIdWithDeletedAsync(id);

            this.genreRepository.Delete(genre);
            await this.genreRepository.SaveChangesAsync();
        }

        public async Task HardDeleteGenreAsync(int id)
        {
            var genre = await this.genreRepository.GetByIdWithDeletedAsync(id);

            var genreMoviesToDelete = this.movieGenreRepository.All().Where(x => x.GenreId == id);
            foreach (var genreToDelete in genreMoviesToDelete)
            {
                this.movieGenreRepository.Delete(genreToDelete);
            }

            this.genreRepository.HardDelete(genre);
            await this.genreRepository.SaveChangesAsync();
        }

        public async Task<int> EditGenreAsync(int id, string genreName)
        {
            var genre = await this.genreRepository.GetByIdWithDeletedAsync(id);

            if (genreName.Length < 2 || genreName.Length > 15)
            {
                throw new ArgumentOutOfRangeException("Name should be between 2 and 15 characters");
            }

            genre.Name = genreName;

            this.genreRepository.Update(genre);
            await this.genreRepository.SaveChangesAsync();

            return genre.Id;
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

        public async Task<int> CreateGenreAsync(string name)
        {
            var genre = new Genre
            {
                Name = name,
            };

            if (this.genreRepository.All().Any(x => x.Name == name))
            {
                return 0;
            }

            await this.genreRepository.AddAsync(genre);
            await this.genreRepository.SaveChangesAsync();

            return genre.Id;
        }
    }
}
