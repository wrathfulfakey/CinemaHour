namespace CinemaHour.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using CinemaHour.Data;
    using CinemaHour.Data.Models;
    using CinemaHour.Data.Repositories;
    using CinemaHour.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UsersServiceTests
    {
        // [Fact]
        // public async Task GetByUsernameShouldReturnUser()
        // {
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //          .UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    var repository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

        // await repository.AddAsync(new ApplicationUser { Id = "test1", UserName = "TestUser1" });
        //    await repository.AddAsync(new ApplicationUser { Id = "test2", UserName = "TestUser2" });
        //    await repository.AddAsync(new ApplicationUser { Id = "test3", UserName = "TestUser3" });

        // await repository.SaveChangesAsync();
        //    var service = new UsersService(repository, null, null, null, null);

        // AutoMapperConfig.RegisterMappings(typeof(UserTestModel).Assembly);

        // var user = service.GetByUsername<UserTestModel>("TestUser2");

        // Assert.Equal("test2", user.Id);
        //    Assert.Equal("TestUser2", user.UserName);
        // }

        // public class UserTestModel : IMapFrom<ApplicationUser>
        // {
        //    public string Id { get; set; }

        // public string UserName { get; set; }
        // }
    }
}
