using LocationManageCore.Data;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using TP1.Models.Adresses;
using TP1.Models.Locations;

namespace TP1.Controllers
{
    public class AddressesController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext Context = context;





        //[HttpGet]
        ////[Authorize(Roles = Roles.MANAGER)]

        //public async Task<IActionResult> Create(LocationCreate model)
        //{
        //    return View(model);
        //}

        //[HttpPost]
        ////[Authorize(Roles = Roles.MANAGER)]
        ////[Authorize(Roles = Roles.ADMIN)]
        ////[Authorize(Roles = Roles.CLERK)]


        //public async Task<IActionResult> Create(AddressCreate model)
        //{
        //    //ModelState.Remove("Driver");
        //    //ModelState.Remove("Car");

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var address = Address.Create(
        //        model.StreetNumber,
        //        model.StreetName,
        //        model.CityName,
        //        model.Province,
        //        model.Country,
        //        model.PostalCode);

        //    Context.Addresses.Add(address);
        //    await Context.SaveChangesAsync();
        //    return RedirectToAction("List", "Car", new { branchId = BranchId });
        //}
    }
}
