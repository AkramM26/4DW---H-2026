using LocationManageCore.Data;
using LocationManageCore.Domains;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TP1.Models.Drivers;
namespace TP1.Controllers
{
    [Authorize]

    public class DriversController : Controller
    {
        // Réference à DbContext
        private readonly ApplicationDbContext _context;

        public DriversController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var driver = await _context.Drivers
                .Include(d => d.Locations)
                    .ThenInclude(l => l.Car)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null)
            {
                return RedirectToAction("Index", new { erreur = "Le système ne parvient pas à afficher le profil du conducteur." });
            }

            return View(driver);
        }

        public async Task<IActionResult> Index()
        {
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> List(string erreur = null)
        {
            var drivers = await _context.Drivers
                .Include(d => d.Locations)
                    .ThenInclude(l => l.Car)
                .ToListAsync();

            if (!string.IsNullOrEmpty(erreur))
            {
                ViewData["Erreur"] = erreur;
            }

            return View(drivers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DriverCreate model)
        {
            try
            {
                if (!ModelState.IsValid) // revoir les validations 
                {
                    return View(model);
                }

                var driver = Driver.Create(
                    model.FirstName!,
                    model.LastName!,
                    model.EmailAdress!,
                    model.PhoneNumber,
                    model.DriverLicenceNumber
                    );
                _context.Drivers.Add(driver);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Le conducteur a été ajouter avec succès.";
                return RedirectToAction(nameof(List));
            }
            catch (Exception)
            {
                TempData["Erreur"] = "Erreur : Impossible d'ajouter le conducteur'";
                return RedirectToAction(nameof(List));
            }
        }
    }
}