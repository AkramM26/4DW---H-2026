using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TP1.Models.ApplicationUsers;

namespace TP1.Controllers
{
    public class UserController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager) 
        : Controller
    {
        private readonly UserManager<IdentityUser> UserManager = userManager;
        private readonly SignInManager<IdentityUser> SignInManager = signInManager;

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreate vm)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(vm);
            //}

            //if (vm.FullName == "Michel Tremblay")
            //    ModelState.AddModelError("", "Non! t'es banni");

            //// Créer l'entité utilisateur
            //var newUser = new IdentityUser(vm.UserName!)
            //{
            //    Email = vm.EmailAddress
            //};

            //// Ajouter l'utilisate
            //// ur au contexte via le service d'identité
            //var result = await UserManager.CreateAsync(newUser, vm.Password!);

            //if (!result.Succeeded)
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError(string.Empty, error.Description);
            //    }

            //    return View(vm);
            //}
            //// Rediriger vers la page de connexion OU page d'acceuil
            //var signInResult = await SignInManager.PasswordSignInAsync(
            //    newUser, vm.Password!,
            //    isPersistent: false, // rememberMe?
            //    lockoutOnFailure: false
            //);

            //if (!signInResult.Succeeded)
            //{
            //    ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la connexion.");
            //    return View(vm);
            //}

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET : /Account/Login(returnUrl)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST : /Account/Login(vm)
        [HttpPost]
        public IActionResult Login(UserCreate vm)
        {
            return View();
        }

        // GET : /Account/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
