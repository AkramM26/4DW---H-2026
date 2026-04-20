using LocationManageCore.Data;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TP1.Constantes;

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
            // ajout : récup du contexte pour avoir accès aux succursales
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var Admin = await userManager.FindByNameAsync("Admin");

            if (Admin == null)
            {
                // 1. Cherche si il existe déjà une succursale
                var defaultBranch = await context.Branches.FirstOrDefaultAsync();

                // 2. Si aucune n'existe, on en crée une (car sinon  un Admin ne pourra pas être créé)
                if (defaultBranch == null)
                {
                    defaultBranch = Branch.Create(true, "Siège Social");
                    context.Branches.Add(defaultBranch);
                    await context.SaveChangesAsync();
                }

                // création de l'admin
                Admin = AppUser.Create("Admin", "Administrateur", "admin@gmail.com");

                await userManager.CreateAsync(Admin, "L'Admin1234*");
                await userManager.AddToRoleAsync(Admin, Roles.ADMIN);
            }

        }
    }
}

