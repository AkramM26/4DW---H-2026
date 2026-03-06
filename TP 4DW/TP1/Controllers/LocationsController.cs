using LocationManageCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TP1.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Le constructeur reçoit la DB
        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allLocations = await _context.Locations
                .Include(l => l.Car)
                .Include(l => l.Driver)
                .ToListAsync();

            // UC15, séparation des loc. courantes et passées
            ViewBag.Courantes = allLocations.Where(l => l.OfficialClosing == null).ToList();
            ViewBag.Passees = allLocations.Where(l => l.OfficialClosing != null).ToList();
            return View();
        }

        // UC16, consulter une location
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(l => l.Car)
                .Include(l => l.Driver)
                    .ThenInclude(d => d.Address) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }
    }
}
