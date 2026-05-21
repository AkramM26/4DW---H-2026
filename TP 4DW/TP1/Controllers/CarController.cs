using LocationManageCore.Data;
using LocationManageCore.Domains;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP1.Constantes;
using TP1.Models.Cars;

namespace TP1.Controllers
{
    //[Authorize(Roles = Roles.MANAGER + "," + Roles.ADMIN)]


    public class CarController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext Context = context;
        [Authorize(Roles = Roles.CLERK + "," + Roles.MANAGER + "," + Roles.ADMIN)]
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(ListAll));
        }

        [Authorize(Roles = Roles.CLERK + "," + Roles.MANAGER + "," + Roles.ADMIN)]

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

            if (model.Year < 2000 || model.Year > DateTime.Now.Year + 1)
            {
                ModelState.AddModelError(nameof(model.Year), "L'année doit être entre 2000 et 2027.");
                return View(model);
            }

            var car = Car.Create(
                model.CarBrand!.Trim(),
                model.CarModel!.Trim(),
                model.Year!.Value,
                model.Color!.Trim(),
                model.SerialNumber!.ToUpper(),
                model.Registration!.ToUpper(),
                model.Mileage!.Value,
                model.Nickname!,
                model.EstimatedValue!.Value,
                model.BranchId
                );
            Context.Cars.Add(car);

            if (!string.IsNullOrWhiteSpace(model.InitialNote))
            {
                var note = Note.CreateForCar(model.InitialNote.Trim(), car.Id);
                Context.Notes.Add(note);
            }

            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(List), new { branchId = branchId });
        }
        [Authorize(Roles = Roles.CLERK + "," + Roles.MANAGER + "," + Roles.ADMIN)]
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

        [Authorize(Roles = Roles.CLERK + "," + Roles.MANAGER + "," + Roles.ADMIN)]
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

        [Authorize(Roles = Roles.CLERK + "," + Roles.MANAGER + "," + Roles.ADMIN)]
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

        [Authorize(Roles = Roles.CLERK + "," + Roles.MANAGER + "," + Roles.ADMIN)]
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

            if (model.Year < 2000 || model.Year > DateTime.Now.Year + 1)
            {
                ModelState.AddModelError(nameof(model.Year), "L'année doit être entre 2000 et 2027.");
                return View(model);
            }

            car.CarModel = model.CarModel!.Trim();
            car.CarBrand = model.CarBrand!.Trim();
            car.Nickname = model.Nickname!;
            car.Status = model.Status!;
            car.Availability = model.Availability!;
            car.State = model.State!;
            car.SerialNumber = model.SerialNumber!;
            car.Registration = model.Registration!;
            car.Year = model.Year!.Value;
            car.Color = model.Color!.Trim();
            car.Mileage = model.Mileage!.Value;
            car.EstimatedValue = model.EstimatedValue!.Value;

            await Context.SaveChangesAsync();

            return RedirectToAction(nameof(List), new { branchId = branchId });
        }

        [Authorize(Roles = Roles.CLERK + "," + Roles.MANAGER + "," + Roles.ADMIN)]
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await Context.Cars
                .Include(c => c.Locations)
                    .ThenInclude(l => l.Driver)
                .Include(c => c.Notes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car is null)
            {
                TempData["Error"] = "Le système ne parvient pas à afficher le profil de la voiture.";
                return RedirectToAction(nameof(ListAll));
            }
            return View(car);
        }

        // Ajouter une note à une voiture
        [Authorize(Roles = Roles.CLERK + "," + Roles.MANAGER + "," + Roles.ADMIN)]
        [HttpGet]
        public async Task<IActionResult> AddNote(Guid id)
        {
            var car = await Context.Cars.FindAsync(id);
            if (car is null) return NotFound();

            ViewBag.CarId = id;
            ViewBag.CarNickname = car.Nickname;
            return View();
        }
        [Authorize(Roles = Roles.CLERK + "," + Roles.MANAGER + "," + Roles.ADMIN)]
        [HttpPost]
        public async Task<IActionResult> AddNote(Guid id, string content)
        {
            var car = await Context.Cars.Include(c => c.Notes).FirstOrDefaultAsync(c => c.Id == id);
            if (car is null) return NotFound();

            if (string.IsNullOrWhiteSpace(content))
            {
                ViewBag.CarId = id;
                ViewBag.CarNickname = car.Nickname;
                ModelState.AddModelError("content", "Le contenu de la note est obligatoire.");
                return View();
            }

            var note = Note.CreateForCar(content.Trim(), id);
            Context.Notes.Add(note);
            await Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Note ajoutée avec succès.";
            return RedirectToAction(nameof(Details), new { id });
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