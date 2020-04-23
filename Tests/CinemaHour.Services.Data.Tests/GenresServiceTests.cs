namespace CinemaHour.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CinemaHour.Data;
    using CinemaHour.Data.Models;
    using CinemaHour.Data.Repositories;
    using CinemaHour.Services.Data;
    using CinemaHour.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class GenresServiceTests
    {
        [Theory]
        [InlineData("a")]
        [InlineData("aaaaaaaaaaaaaaaa")]
        public async Task EditGenreMustReturnFalseWithIncorrectNames(string name)
        {
            AutoMapperConfig.RegisterMappings(typeof(GenreTestModel).Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Genre>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Genre { Id = 1, Name = "Action" });
            await repository.SaveChangesAsync();
            var service = new GenresService(repository, null);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.EditGenreAsync(1, name));
        }

        [Fact]
        public async Task EditGenreMustRenameGenreByGiveId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Genre>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Genre { Id = 1, Name = "Action" });
            await repository.SaveChangesAsync();
            var service = new GenresService(repository, null);

            AutoMapperConfig.RegisterMappings(typeof(GenreTestModel).Assembly);

            var genre = await service.EditGenreAsync(1, "Drama");
            var expectedGenre = service.GetById<GenreTestModel>(1);
            Assert.Equal(1, genre);
            Assert.Equal("Drama", expectedGenre.Name);
        }

        [Fact]
        public async Task GetGenreByIdAndReturnIdAndNameIfItExists()
        {
            AutoMapperConfig.RegisterMappings(typeof(GenreTestModel).Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Genre>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Genre { Id = 1, Name = "Action" });
            await repository.AddAsync(new Genre { Id = 2, Name = "Comedy" });
            await repository.AddAsync(new Genre { Id = 3, Name = "Drama" });

            await repository.SaveChangesAsync();
            var service = new GenresService(repository, null);

            var genre = service.GetById<GenreTestModel>(1);

            Assert.Equal(1, genre.Id);
            Assert.Equal("Action", genre.Name);
        }

        [Fact]
        public async Task GetAllGenresWhenCalled()
        {
            AutoMapperConfig.RegisterMappings(typeof(GenreTestModel).Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Genre>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Genre { Id = 4, Name = "Action" });
            await repository.AddAsync(new Genre { Id = 10, Name = "Comedy" });
            await repository.AddAsync(new Genre { Id = 11, Name = "Drama" });

            await repository.SaveChangesAsync();
            var service = new GenresService(repository, null);

            var genresCount = service.GetAll<GenreTestModel>().Count;

            Assert.Equal(3, genresCount);
        }

        [Fact]
        public async Task DeleteGenreShouldDeleteGenreByName()
        {
            AutoMapperConfig.RegisterMappings(typeof(GenreTestModel).Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Genre>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Genre { Id = 1, Name = "Drama" });
            await repository.AddAsync(new Genre { Id = 2, Name = "Comedy" });
            await repository.AddAsync(new Genre { Id = 3, Name = "Action" });

            await repository.SaveChangesAsync();
            var service = new GenresService(repository, null);

            await service.DeleteGenreAsync(2);
            var genresCount = service.GetAll<GenreTestModel>().Count;
            Assert.Equal(2, genresCount);
        }

        [Fact]
        public async Task CreateGenreShouldBeCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(GenreTestModel).Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Genre>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Genre { Id = 1, Name = "Drama" });
            await repository.AddAsync(new Genre { Id = 2, Name = "Comedy" });

            await repository.SaveChangesAsync();
            var service = new GenresService(repository, null);

            await service.CreateGenreAsync("Action");
            var genresCount = service.GetAll<GenreTestModel>().Count;
            Assert.Equal(3, genresCount);
        }

        public class GenreTestModel : IMapFrom<Genre>
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
