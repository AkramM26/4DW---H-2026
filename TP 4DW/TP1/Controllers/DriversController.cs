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

        public async Task<IActionResult> Index()
        {
            var drivers = await _context.Drivers
                .Include(d => d.Locations)
                    .ThenInclude(l => l.Car)
                .ToListAsync();

            return View(drivers);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var driver = await _context.Drivers
                .Include(d => d.Locations)
                    .ThenInclude(l => l.Car)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }
    }
}