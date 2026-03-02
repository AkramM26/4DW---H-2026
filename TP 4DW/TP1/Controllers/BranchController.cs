using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1.Data;
using TP1.Models;

namespace TP1.Controllers;

public class BranchController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext Context = context;

    [HttpPost]
    public async Task<IActionResult> Create(Branch model)
    {
        if (!ModelState.IsValid) { 
            return View(model);
        }
        var branch = Branch.Create(
            model.Status,
            model.Name);

        Context.Branches.Add(branch);
        await Context.SaveChangesAsync();

        return RedirectToAction(nameof(List));
    }

    public async Task<IActionResult> List()
    {
        var contacts = await Context.Branches.ToListAsync();

        return View(contacts);
    }

    public IActionResult Index()
    {
        return RedirectToAction("List");
    }
}
