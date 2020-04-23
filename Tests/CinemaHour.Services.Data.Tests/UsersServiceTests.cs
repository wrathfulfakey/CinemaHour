namespace CinemaHour.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using CinemaHour.Data;
    using CinemaHour.Data.Models;
    using CinemaHour.Data.Repositories;
    using CinemaHour.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class UsersServiceTests
    {
        public static Mock<UserManager<ApplicationUser>> MockUserManager<TUser>(List<ApplicationUser> ls)
            where TUser : class
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<ApplicationUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

        [Fact]
        public async Task GetByUsernameShouldReturnUser()
        {
            AutoMapperConfig.RegisterMappings(typeof(UserTestModel).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new ApplicationUser { Id = "test1", UserName = "TestUser1" });
            await repository.AddAsync(new ApplicationUser { Id = "test2", UserName = "TestUser2" });
            await repository.AddAsync(new ApplicationUser { Id = "test3", UserName = "TestUser3" });

            await repository.SaveChangesAsync();
            var service = new UsersService(repository, null, null, null, null);

            var user = service.GetByUsername<UserTestModel>("TestUser2");

            Assert.Equal("test2", user.Id);
            Assert.Equal("TestUser2", user.UserName);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("AnotherTest")]
        public async Task GetByUsernameWithInvalidInputShouldReturnNullException(string username)
        {
            AutoMapperConfig.RegisterMappings(typeof(UserTestModel).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new ApplicationUser { Id = "test1", UserName = "TestUser1" });
            await repository.AddAsync(new ApplicationUser { Id = "test2", UserName = "TestUser2" });
            await repository.AddAsync(new ApplicationUser { Id = "test3", UserName = "TestUser3" });

            await repository.SaveChangesAsync();
            var service = new UsersService(repository, null, null, null, null);

            Assert.Throws<NullReferenceException>(() => service.GetByUsername<UserTestModel>(username));
        }

        [Fact]
        public async Task LockdownUserAsyncShouldBanUserForSevenDays()
        {
            AutoMapperConfig.RegisterMappings(typeof(UserTestModel).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new ApplicationUser { Id = "test1", UserName = "TestUser1" });

            await repository.SaveChangesAsync();
            var mockedUserManager = MockUserManager<ApplicationUser>(repository.All().ToList());
            var service = new UsersService(repository, null, null, null, mockedUserManager.Object);

            await service.LockdownUserAsync("TestUser1");
            var user = service.GetByUsername<UserTestModel>("TestUser1");

            Assert.True(user.LockoutEnabled);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("anotherTest")]
        public async Task LockdownUserAsyncShouldReturnUserNotFoundMessage(string output)
        {
            AutoMapperConfig.RegisterMappings(typeof(UserTestModel).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(new ApplicationUser { Id = "test1", UserName = "TestUser1" });

            await repository.SaveChangesAsync();
            var mockedUserManager = MockUserManager<ApplicationUser>(repository.All().ToList());
            var service = new UsersService(repository, null, null, null, mockedUserManager.Object);

            var outputMessage = await service.LockdownUserAsync(output);

            Assert.Equal("User not found", outputMessage);
        }

        public class UserTestModel : IMapFrom<ApplicationUser>
        {
            public string Id { get; set; }

            public string UserName { get; set; }

            public bool LockoutEnabled { get; set; }
        }
    }
}
