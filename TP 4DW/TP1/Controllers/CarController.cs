using LocationManageCore.Data;
using LocationManageCore.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP1.Constantes;
using TP1.Models.Cars;

namespace TP1.Controllers
{
    [Authorize(Roles = Roles.MANAGER + "," + Roles.ADMIN)]


    public class CarController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext Context = context;

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(ListAll));
        }


        [HttpGet]

        public IActionResult Create(Guid branchId)
        {
            return View(CarCreateFactory.Create(branchId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreate model, Guid branchId)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var car = Car.Create(
                model.CarBrand!,
                model.CarModel!,
                model.Year,
                model.Color!,
                model.SerialNumber!,
                model.Registration!,
                model.Mileage!,
                model.Nickname!,
                model.EstimatedValue!,
                model.BranchId
                );
            Context.Cars.Add(car);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(List), new { branchId = branchId });
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var cars = await Context.Cars
                .Where(car => car.Status != "Archivé")
                .Select(car => new CarItem
                {
                    Id = car.Id,
                    BranchId = car.BranchId,
                    Nickname = car.Nickname,
                    Status = car.Status,
                    Availability = car.Availability,
                    State = car.State,
                    SerialNumber = car.SerialNumber,
                    CarBrand = car.CarBrand,
                    Color = car.Color,
                    CarModel = car.CarModel,
                    Registration = car.Registration,
                    Year = car.Year,
                    Mileage = car.Mileage,
                    EstimatedValue = car.EstimatedValue
                }).ToListAsync();
            return View(cars);
        }

        [HttpGet]
        public async Task<IActionResult> List(Guid branchId)
        {
            var branch = await Context.Branches
                .Include(b => b.Cars.Where(c => c.Status != "Archivé"))
                .FirstOrDefaultAsync(b => b.BranchId == branchId);

            if (branch == null)
            {
                return NotFound();
            }
            ViewBag.BranchId = branchId;

            return View(branch.Cars.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, Guid branchId)
        {
            var car = await Context.Cars.FindAsync(id);
            if (car is null) return NotFound();

            ViewBag.BranchId = branchId;

            var model = new CarEdit
            {
                Id = car.Id,
                BranchId = car.BranchId,
                Nickname = car.Nickname,
                Status = car.Status,
                Availability = car.Availability,
                State = car.State,
                SerialNumber = car.SerialNumber,
                CarBrand = car.CarBrand,
                Color = car.Color,
                CarModel = car.CarModel,
                Registration = car.Registration,
                EstimatedValue = car.EstimatedValue,
                Year = car.Year,
                Mileage = car.Mileage
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarEdit model, Guid branchId)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var car = await Context.Cars.FindAsync(model.Id);
            if (car is null)
            {
                return NotFound();
            }

            car.CarModel = model.CarModel!;
            car.CarBrand = model.CarBrand!;
            car.Nickname = model.Nickname!;
            car.Status = model.Status!;
            car.Availability = model.Availability!;
            car.State = model.State!;
            car.SerialNumber = model.SerialNumber!;
            car.Registration = model.Registration!;
            car.Year = model.Year!;
            car.Color = model.Color!;
            car.Mileage = model.Mileage!;
            car.EstimatedValue = model.EstimatedValue!;



            await Context.SaveChangesAsync();

            return RedirectToAction(nameof(List), new { branchId = branchId });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await Context.Cars
                .Include(c => c.Locations)
                    .ThenInclude(l => l.Driver)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car is null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Archive(Guid id, Guid? branchId)
        {
            var car = await Context.Cars.FindAsync(id);
            if (car is null) return NotFound();

            if (car.Status == "Actif")
            {
                TempData["Error"] = "Le véhicule doit être désactivé avant de pouvoir être archivé.";
            }
            else
            {
                car.Status = "Archivé";
                await Context.SaveChangesAsync();
            }

            if (branchId.HasValue && branchId != Guid.Empty)
            {
                return RedirectToAction(nameof(List), new { branchId = branchId });
            }

            return RedirectToAction(nameof(ListAll));
        }

        [HttpGet]
        public async Task<IActionResult> ArchivedList()
        {
            var archivedCars = await Context.Cars
                .Where(c => c.Status == "Archivé")
                .Select(c => new CarItem
                {
                    Id = c.Id,
                    Nickname = c.Nickname,
                    CarBrand = c.CarBrand,
                    CarModel = c.CarModel,
                    Registration = c.Registration,
                    Status = c.Status,
                    Year = c.Year,
                    State = c.State,
                    Availability = c.Availability,
                    Color = c.Color,
                    Mileage = c.Mileage,
                    EstimatedValue = c.EstimatedValue,
                    SerialNumber = c.SerialNumber
                }).ToListAsync();

            return View(archivedCars);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(Guid id)
        {
            var car = await Context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            car.Status = "Actif";
            await Context.SaveChangesAsync();

            TempData["Success"] = $"Le véhicule {car.Nickname} a été restauré avec succès.";
            return RedirectToAction(nameof(ArchivedList));
        }

        // Exemple :
        // GET: Cars/Transfer/5
        //[Authorize(Roles = "MANAGER, ADMIN")]
        public async Task<IActionResult> Transfer(Guid? id)
        {
            if (id == null) return NotFound();

            var car = await context.Cars
                .Include(c => c.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null) return NotFound();

            // Préparation de la liste des succursales pour menu déroulant
            ViewBag.Branches = await context.Branches.ToListAsync();
            return View(car);
        }

        // POST: Cars/Transfer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "MANAGER, ADMIN")]
        public async Task<IActionResult> Transfer(Guid id, Guid newBranchId)
        {
            var car = await context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            // pour UC21
            car.BranchId = newBranchId;

            await context.SaveChangesAsync();

            TempData["Success"] = $"Le véhicule {car.Nickname} a été transféré avec succès.";

            return RedirectToAction("List", new { branchId = car.BranchId });
        }
    }
}