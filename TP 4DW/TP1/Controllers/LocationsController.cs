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
using System.Xml.Linq;
using TP1.Constantes;
using TP1.Models.Account;
using TP1.Models.Cars;
using TP1.Models.Locations;

namespace TP1.Controllers
{
    [Authorize(Roles = Roles.MANAGER + "," + Roles.ADMIN + "," + Roles.CLERK)]

    public class LocationsController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext Context = context;

        [HttpGet]
        public async Task<IActionResult> Create(LocationCreate model, Guid BrandId)
        {
            return View (LocationCreateFactory.Create(BrandId));
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
                .Include(l => l.Notes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return RedirectToAction("Index", new { erreur = "Le système ne parvient pas à afficher le profil de la location." });
            }

            return View(location);
        }

        // Ajouter une note à une location
        [HttpGet]
        public async Task<IActionResult> AddNote(Guid id)
        {
            var location = await Context.Locations
                .Include(l => l.Car)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (location == null)
                return RedirectToAction(nameof(Index), new { erreur = "Location introuvable." });

            ViewBag.LocationId = id;
            ViewBag.CarNickname = location.Car?.Nickname;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Guid id, string content)
        {
            var location = await Context.Locations
                .Include(l => l.Notes)
                .Include(l => l.Car)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (location == null)
                return RedirectToAction(nameof(Index), new { erreur = "Location introuvable." });

            if (string.IsNullOrWhiteSpace(content))
            {
                ViewBag.LocationId = id;
                ViewBag.CarNickname = location.Car?.Nickname;
                ModelState.AddModelError("content", "Le contenu de la note est obligatoire.");
                return View();
            }

            var note = Note.Create(content);
            note.LocationId = id;
            Context.Notes.Add(note);
            await Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Note ajoutée avec succès.";
            return RedirectToAction(nameof(Details), new { id });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(LocationDelete model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LocationDelete model, Guid? id, Guid BranchId)
        {
            var car = await Context.Cars.FindAsync(id);
            var location = Context.Locations.Where(l => l.CarId == id).FirstOrDefault();

            if (location == null) 
                return NotFound();  

            location.OfficialClosing= model.OfficialClosing;
            car.Availability = true;
            var note = Note.Create(model.Note);
            Context.SaveChangesAsync();


            location.Notes.Add(note);

            Context.SaveChangesAsync();


            TempData["SuccessMessage"] = "La location a été fermée avec succès ";

            return RedirectToAction("List", "Car", new { branchId = BranchId });


        }


    }
}