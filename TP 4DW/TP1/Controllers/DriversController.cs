using LocationManageCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace TP1.Controllers
{
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

        public async Task<IActionResult> Index(string erreur = null)
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
    }
}