using LocationManageCore.Data;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TP1.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // UC15 : liste des locations
        public async Task<IActionResult> Index(string erreur = null)
        {
            var allLocations = await _context.Locations
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

            var location = await _context.Locations
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
    }
}