using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using TP1.Constantes;

namespace LocationManagerCore.Data
{
    public class RolesSeed(UserManager<AppUser> userManager)
    {
        private readonly UserManager<AppUser> UserManager = userManager;

        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roles = { Roles.ADMIN, Roles.MANAGER, Roles.CLERK, Roles.USER };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>
                    {
                        Name = role
                    });
                }
            }
        }

        public static async Task SeedAdminsAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();


            var Admin = await userManager.FindByNameAsync("Admin");

            if (Admin == null)
            {
                Admin = AppUser.Create("Admin", "", "admin@gmail.com");

                await userManager.CreateAsync(Admin, "L'Admin1234*");

                await userManager.AddToRoleAsync(Admin, Roles.ADMIN);
            }
        }

    }
}

