using LocationManageCore.Data;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TP1.Controllers
{
    [Authorize]

    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public LocationsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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