#define SeedOnly
#if SeedOnly

using src.Models;
using src.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace src.Data
{
    public static class SeedData
    {
        #region snippet_Initialize
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using(var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
                {
                    //For sample purposes we are seeding 2 users both with the same
                    //password. The password is set with the following command
                    //dotnet user-secrets set SeedUserPW <pw>
                    //The admin user can do anything

                    var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@busco.com");
                    await EnsureRole(serviceProvider, adminID, Constants.BusCompanyAdministratorsRole);

                    //allowed user can create and modify routes and assign drivers
                    var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@busco.com");
                    await EnsureRole(serviceProvider, managerID, Constants.BusCompanyManagersRole);

                    var driverID = await EnsureUser(serviceProvider, testUserPw, "driver@busco.com");
                    await EnsureRole(serviceProvider, driverID, Constants.BusCompanyDriversRole);
                }
        }

        #endregion

        #region snippet_CreateRoles
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);

            if(user == null)
            {
                user = new ApplicationUser{UserName = UserName};
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if(!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        #endregion
    }
}
#endif