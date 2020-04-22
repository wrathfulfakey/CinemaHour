namespace CinemaHour.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CinemaHour.Data;
    using CinemaHour.Data.Models;
    using CinemaHour.Data.Repositories;
    using CinemaHour.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class DirectorsServiceTests
    {
        [Fact]
        public async Task GetAllShouldReturnAllDirectors()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Director>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Director { Id = "1", FullName = "John Doe" });
            await repository.AddAsync(new Director { Id = "2", FullName = "John Doe2" });
            await repository.AddAsync(new Director { Id = "3", FullName = "John Doe3" });

            await repository.SaveChangesAsync();
            var directorsService = new DirectorsService(repository, null);
            AutoMapperConfig.RegisterMappings(typeof(DirectorTestModel).Assembly);
            ICollection<DirectorTestModel> directorsCount = directorsService.GetAll<DirectorTestModel>();

            Assert.Equal(3, directorsCount.Count);
        }

        [Fact]
        public async Task GetDirectorById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Director>(new ApplicationDbContext(options.Options));
            var movieDiretorsRepository = new EfRepository<MovieDirector>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Director { Id = "1", FullName = "John Doe" });

            await repository.SaveChangesAsync();
            var directorsService = new DirectorsService(repository, movieDiretorsRepository);
            AutoMapperConfig.RegisterMappings(typeof(DirectorTestModel).Assembly);
            var director = directorsService.GetById<DirectorTestModel>("1");

            Assert.Equal("John Doe", director.FullName);
        }

        [Fact]
        public async Task GetDirectorByIdShouldReturnIncorrect()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Director>(new ApplicationDbContext(options.Options));
            var movieDiretorsRepository = new EfRepository<MovieDirector>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Director { Id = "1", FullName = "John Doe" });
            await repository.AddAsync(new Director { Id = "2", FullName = "John Doe2" });

            await repository.SaveChangesAsync();
            var directorsService = new DirectorsService(repository, movieDiretorsRepository);
            AutoMapperConfig.RegisterMappings(typeof(DirectorTestModel).Assembly);
            var director = directorsService.GetById<DirectorTestModel>("2");

            bool isTheSameDirector = director.FullName == "John Doe";

            Assert.False(isTheSameDirector);
        }

        [Fact]
        public async Task DeleteDirectorCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Director>(new ApplicationDbContext(options.Options));
            var movieDiretorsRepository = new EfRepository<MovieDirector>(new ApplicationDbContext(options.Options));

            var directorRep = new Director { FullName = "John DoeId" };
            await repository.AddAsync(directorRep);
            await repository.AddAsync(new Director { FullName = "JohnDoe" });
            await repository.AddAsync(new Director { FullName = "John Doe" });

            await repository.SaveChangesAsync();
            var directorsService = new DirectorsService(repository, movieDiretorsRepository);
            AutoMapperConfig.RegisterMappings(typeof(DirectorTestModel).Assembly);
            await directorsService.DeleteDirectorAsync(directorRep.Id);
            var directorsCount = directorsService.GetAll<DirectorTestModel>().Count;

            Assert.Equal(2, directorsCount);
        }

        [Fact]
        public async Task EditDirectorShouldRenameDirector()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Director>(new ApplicationDbContext(options.Options));
            var movieDiretorsRepository = new EfRepository<MovieDirector>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new Director { Id = "1", FullName = "John Doe" });

            await repository.SaveChangesAsync();
            var directorsService = new DirectorsService(repository, movieDiretorsRepository);
            AutoMapperConfig.RegisterMappings(typeof(DirectorTestModel).Assembly);

            await directorsService.EditDirectorAsync("1", "Doe John");

            var directorNameAfterEdit = directorsService.GetById<DirectorTestModel>("1");

            Assert.Equal("Doe John", directorNameAfterEdit.FullName);
        }

        public class DirectorTestModel : IMapFrom<Director>
        {
            public string FullName { get; set; }
        }
    }
}
