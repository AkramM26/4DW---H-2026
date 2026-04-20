using LocationManageCore.Data;
using LocationManageCore.Domains;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TP1.Constantes;
using TP1.Models.Cars;
using TP1.Models.Locations;

namespace TP1.Controllers
{
    [Authorize(Roles = Roles.MANAGER + "," + Roles.ADMIN + "," + Roles.CLERK)]

    public class LocationsController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public LocationsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationCreate model, int a)
        {
            //ModelState.Remove("Driver");
            //ModelState.Remove("Car");

            //RedirectToAction("CreateInLocation", "Addresses", new { branchId = BranchId });


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var address = Address.Create
                (model.StreetNumber,
                model.StreetName,
                model.CityName,
                model.Province,
                model.Country,
                model.PostalCode);
            Context.Addresses.Add(address);
            await Context.SaveChangesAsync();



            var driver = Driver.Create(
                model.DriverFirstName,
                model.DriverLastname,
                model.DriverEmailAddress,
                model.DriverLicenceNumber,
                model.PhoneNumber,
                address.Id
                );
            Context.Drivers.Add(driver);
            await Context.SaveChangesAsync();



            var location = Location.Create(
                model.CarId,
                driver.Id,
                model.Opening,
                model.PlanedClosing
                );
            Context.Locations.Add(location);


            //Mise à jour de la disponibilté de la voiture
            var car = await Context.Cars.FindAsync(model.CarId);
            car.Availability = false;

            //Affectations
            //model.CarId= CarId;
            model.DriverId = driver.Id;

            await Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "La location a été créée avec succès pour " + model.DriverFirstName + " " + model.DriverLastname;
            return RedirectToAction("List", "Car", new { branchId = model.BranchId });
        }

        // UC15 : liste des locations
        public async Task<IActionResult> Index(string erreur = null)
        {
            var allLocations = await Context.Locations
                .Include(l => l.Car)
                .Include(l => l.Driver)
                .ToListAsync();

            ViewBag.Courantes = allLocations.Where(l => l.OfficialClosing == null).ToList();
            ViewBag.Passees = allLocations.Where(l => l.OfficialClosing != null).ToList();

            // Passage du message d'erreur à la vue via ViewData
            if (!string.IsNullOrEmpty(erreur))
            {
                ViewData["Erreur"] = erreur;
            }

            return View();
        }

        // UC16 : consulter une location
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", new { erreur = "Aucune location spécifiée." });
            }

            var location = await Context.Locations
                .Include(l => l.Car)
                .Include(l => l.Driver)
                    .ThenInclude(d => d.Address)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return RedirectToAction("Index", new { erreur = "Le système ne parvient pas à afficher le profil de la location." });
            }

            return View(location);
        }


        [HttpPost]
        public async Task<IActionResult> Close(Guid id)
        {
            var location = await _context.Locations
                .Include(l => l.Car)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (location == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            location.Status = false;
            location.OfficialClosing = DateTime.Now;

            if (location.Car != null)
            {
                location.Car.Availability = true; 
                location.Car.BranchId = user.BranchId; 
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}