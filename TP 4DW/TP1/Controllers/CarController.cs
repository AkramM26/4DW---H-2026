using LocationManageCore.Data;
using LocationManageCore.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using TP1.Models.Cars;

namespace TP1.Controllers
{
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
            return View();
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
                model.BrandId
                );
            Context.Cars.Add(car);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(List), new { branchId = branchId });
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var cars = await Context.Cars
                .Select(car => new CarItem
                {
                    Id = car.Id,
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
                .Include(b => b.Cars)
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

            if (car is null)
            {
                return NotFound();
            }

            var model = new CarEdit
            {
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
            var car = await Context.Cars.FindAsync(id);

            if (car is null)
            {
                return NotFound();
            }

            var model = new CarDetails
            {
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
                Mileage = car.Mileage,


            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, Guid branchId)
        {
            var contact = await Context.Cars.FindAsync(id);

            if (contact is null)
            {
                return NotFound();
            }

            Context.Cars.Remove(contact);
            await Context.SaveChangesAsync();

            return RedirectToAction(nameof(List), new { branchId = branchId });

        }


    }
}
