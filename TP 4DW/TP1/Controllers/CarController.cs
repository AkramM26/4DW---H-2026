using LocationManageCore.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using TP1.Data;
using TP1.Models.Cars;

namespace TP1.Controllers
{
    public class CarController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext Context = context;

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CarCreate model)
        {
            if (!ModelState.IsValid) // revoir les validations 
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
                model.EstimatedValue!
                );
            Context.Cars.Add(car);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> List()
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
                    CarModel = car.CarModel,
                    Mileage = car.Mileage,
                    EstimatedValue = car.EstimatedValue
                }).ToListAsync();
            return View(cars);
        }


    }
}
