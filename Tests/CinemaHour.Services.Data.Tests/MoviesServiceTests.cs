namespace CinemaHour.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data;
    using CinemaHour.Data.Models;
    using CinemaHour.Data.Repositories;
    using CinemaHour.Services.Data.ViewModels.Movies;
    using CinemaHour.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Moq;
    using Xunit;

    public class MoviesServiceTests
    {
        [Fact]
        public async Task CreateMovieShouldAddMovieToRepository()
        {
            AutoMapperConfig.RegisterMappings(typeof(MovieTestModel).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Movie>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Movie { Id = 1, Name = "Movie1" });
            await repository.AddAsync(new Movie { Id = 2, Name = "Movie2" });

            await repository.SaveChangesAsync();
            var service = new MoviesService(repository, null, null, null);

            var movie = new CreateMovieServiceInputModel
            {
                Name = "TestMovie",
                Length = 50,
                Directors = new[] { "Test" },
                Genres = new[] { "1" },
                Actors = new[] { "Test" },
            };

            await service.CreaterMovieAsync(movie);
            var genresCount = service.GetAll<MovieTestModel>().Count;

            Assert.Equal(3, genresCount);
        }

        [Fact]
        public async Task GetByIdShouldReturnValidMovie()
        {
            AutoMapperConfig.RegisterMappings(typeof(MovieTestModel).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Movie>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Movie { Id = 1, Name = "Movie1" });
            await repository.AddAsync(new Movie { Id = 2, Name = "Movie2" });

            await repository.SaveChangesAsync();
            var service = new MoviesService(repository, null, null, null);

            var movie = service.GetById<MovieTestModel>(2);
            Assert.Equal(2, movie.Id);
            Assert.Equal("Movie2", movie.Name);
        }

        [Fact]
        public async Task DeleteMovieShouldDeleteTargetMovieByid()
        {
            AutoMapperConfig.RegisterMappings(typeof(MovieTestModel).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Movie>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Movie { Id = 1, Name = "Movie1" });
            await repository.AddAsync(new Movie { Id = 2, Name = "Movie2" });
            await repository.AddAsync(new Movie { Id = 3, Name = "Movie3" });

            await repository.SaveChangesAsync();
            var service = new MoviesService(repository, null, null, null);

            await service.DeleteMovieAsync(2);
            Assert.Equal(2, repository.All().Count());
        }

        public class MovieTestModel : IMapFrom<Movie>
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
