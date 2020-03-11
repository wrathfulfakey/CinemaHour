namespace CinemaHour.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Common;
    using CinemaHour.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class UserRolesSeeder : ISeeder
    {
        private readonly ApplicationDbContext dbContext;

        public UserRolesSeeder(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await this.SeedUserToRolesAsync(userManager, roleManager);
        }

        private async Task SeedUserToRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var user = await userManager.FindByNameAsync("admin");
            var role = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);

            var exists = this.dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

            if (exists)
            {
                return;
            }

            this.dbContext.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            await this.dbContext.SaveChangesAsync();
        }
    }
}
