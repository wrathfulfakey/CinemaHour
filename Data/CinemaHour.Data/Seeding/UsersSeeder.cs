namespace CinemaHour.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await this.SeedUsersAsync(userManager);
        }

        private async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByNameAsync("admin");

            if (user != null)
            {
                return;
            }

            var result = await userManager.CreateAsync(
                new ApplicationUser
                {
                    AvatarUrl = "https://orbra.institute/wp-content/uploads/avatars/6/5a09e74b09ecc-bpfull.png",
                    UserName = "admin",
                    Email = "wrathfulfakey@gmail.com",
                    EmailConfirmed = true,
                }, "CinemaAdmin1");
        }
    }
}
