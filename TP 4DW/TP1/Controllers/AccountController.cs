using LocationManageCore.Data;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TP1.Constantes;
using TP1.Models.Account;
using TP1.Models.Cars;

namespace TP1.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]


    public class AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ApplicationDbContext context)
        : Controller
    {
        private readonly UserManager<AppUser> UserManager = userManager;
        private readonly SignInManager<AppUser> SignInManager = signInManager;
        private readonly ApplicationDbContext Context = context;

        [HttpGet]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> Register()
        {
            // Récupération des succursales pour une liste déroulante
            ViewBag.Branches = await context.Branches
                .Where(b => b.Status)
                .ToListAsync();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> Register(AccountRegistration vm)
        {
            // BranchId obligatoire pour Gérant et Commis
            if ((vm.Role == Roles.MANAGER || vm.Role == Roles.CLERK) && vm.BranchId == null)
            {
                ModelState.AddModelError(nameof(vm.BranchId), "Une succursale doit être associée pour ce rôle.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Branches = await context.Branches
                    .Where(b => b.Status)
                    .ToListAsync();
                return View(vm);
            }

            // Créer l'entité utilisateur
            var newUser = AppUser.Create(
                    vm.UserName!,
                    vm.FullName!,
                    vm.EmailAddress!
                );

            // Associer la succursale si applicable
            if (vm.BranchId.HasValue)
                newUser.BranchId = vm.BranchId;

            var result = await UserManager.CreateAsync(newUser, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                ViewBag.Branches = await context.Branches
                    .Where(b => b.Status)
                    .ToListAsync();
                return View(vm);
            }

            // Assignation du rôle
            await userManager.AddToRoleAsync(newUser, vm.Role);

            return RedirectToAction(nameof(RegisteredList));
        }

        // GET : /Account/Login(returnUrl)
        [AllowAnonymous]
        [HttpGet]
        public IActionResult LogIn(string? returnUrl = null)
        {
            return View(new LoginViewModel
            { //LogIn
                ReturnUrl = returnUrl
            });
        }

        //POST: /Account/Login(ViewModel)
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var signInResult = await SignInManager.PasswordSignInAsync(
                vm.Username, vm.Password!,
                isPersistent: vm.RememberMe,
                lockoutOnFailure: false);

            if (!signInResult.Succeeded)
                ModelState.AddModelError(string.Empty, " Nom d'utilisateur ou mot de passe invalide");


            if (!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
                return Redirect(vm.ReturnUrl);

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }


        // GET : /Account/Logout
        // GET: /Account/Logout

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> RegisteredList()
        {
            var Users = await UserManager.Users.ToListAsync();
            //var Users = await Context.Users.ToListAsync();
            return View(Users);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string name)
        {
            var user = await UserManager.FindByNameAsync(name);
            await UserManager.DeleteAsync(user);

            return RedirectToAction(nameof(RegisteredList));

        }

   



    }
}
