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

            bool courrielUsernameExiste = await Context.Users.AnyAsync(u => u.EmailAddress == vm.EmailAddress || u.UserName == vm.UserName);
            if (courrielUsernameExiste)
            {
                ModelState.AddModelError(nameof(vm.EmailAddress), "Cette adresse courriel ou ce username est déjà associé à un compte.");
                return View(vm);
            }

            bool roleExiste = await Context.Roles.AnyAsync(r => r.Name == vm.Role);
            if (!roleExiste)
            {
                ModelState.AddModelError(nameof(vm.Role), "Ce rôle n'existe pas.");
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


        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            var user = await UserManager.FindByNameAsync(name);
            if (user == null)
            {
                TempData["Erreur"] = "Utilisateur introuvable.";
                return RedirectToAction(nameof(RegisteredList));
            }

            var result = await UserManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["Erreur"] = "Le système ne parvient pas à supprimer l'utilisateur.";
                return RedirectToAction(nameof(RegisteredList));
            }

            TempData["Message"] = $"L'utilisateur {name} a été supprimé avec succès.";
            return RedirectToAction(nameof(RegisteredList));
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string name)
        {
            var user = await UserManager.FindByNameAsync(name);
            if (user == null)
            {
                TempData["Erreur"] = "Utilisateur introuvable.";
                return RedirectToAction(nameof(RegisteredList));
            }

            var newPassword = GenerateRandomPassword();

            var removeResult = await UserManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
            {
                TempData["Erreur"] = "Le système ne parvient pas à réinitialiser le mot de passe.";
                return RedirectToAction(nameof(RegisteredList));
            }

            var addResult = await UserManager.AddPasswordAsync(user, newPassword);
            if (!addResult.Succeeded)
            {
                TempData["Erreur"] = "Le système ne parvient pas à assigner le nouveau mot de passe.";
                return RedirectToAction(nameof(RegisteredList));
            }

            TempData["NewPassword"] = $"Nouveau mot de passe pour {user.UserName} : {newPassword}";
            return RedirectToAction(nameof(RegisteredList));
        }

        private static string GenerateRandomPassword()
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%";
            const string all = upper + lower + digits + special;

            var random = new Random();
            var pwd = new System.Text.StringBuilder();
            pwd.Append(upper[random.Next(upper.Length)]);
            pwd.Append(lower[random.Next(lower.Length)]);
            pwd.Append(digits[random.Next(digits.Length)]);
            pwd.Append(special[random.Next(special.Length)]);
            for (int i = 4; i < 10; i++)
                pwd.Append(all[random.Next(all.Length)]);

            return new string(pwd.ToString().OrderBy(_ => random.Next()).ToArray());
        }

   



    }
}
