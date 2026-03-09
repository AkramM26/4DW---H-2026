using LocationManageCore.Data;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1.Models.Branches;
using TP1.Models.Cars;


namespace TP1.Controllers;

public class BranchController(ApplicationDbContext context) : Controller
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
    public async Task<IActionResult> Create(BranchCreate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var branch = Branch.Create(
            model.Status,
            model.Name);
        Context.Branches.Add(branch);
        await Context.SaveChangesAsync();
        return RedirectToAction(nameof(List));
    }

    [HttpGet]

    public async Task<IActionResult> List()
    {
        var branch = await Context.Branches
            .Select(branch => new BranchItem
            {
                Id = branch.Id,
                Name = branch.Name,
                Status = branch.Status
            }).ToListAsync();
        return View(branch);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var branch = await Context.Branches
            .Include(b => b.Cars)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (branch == null)
        {
            return NotFound();
        }

        return View(branch.Cars.ToList()); // c'est ici que le type est passé en paramètre
    }


    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var branch = await Context.Branches.FindAsync(id);

        if (branch is null)
        {
            return NotFound();
        }

        var model = new BranchEdit
        {
            Status = branch.Status,
            Name = branch.Name
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BranchEdit model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var branch = await Context.Branches.FindAsync(model.Id);
            if (branch is null)
            {
                return NotFound();
            }

            branch.Status = model.Status;
            branch.Name = model.Name;
            await Context.SaveChangesAsync();
            string action = model.Status ? "activée" : "désactivée";
            TempData["Message"] = $"La succursale a été {action} avec succès.";
            return RedirectToAction(nameof(List));
        }
        catch (Exception)
        {
            TempData["Erreur"] = "Erreur : Impossible de modifier l'état de la succursale.";
            return RedirectToAction(nameof(List));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var branch = await Context.Branches.FindAsync(id);

        if (branch is null)
        {
            return NotFound();
        }

        Context.Branches.Remove(branch);
        await Context.SaveChangesAsync();
        return RedirectToAction(nameof(List));

    }

    [HttpPost]
    public IActionResult ToggleStatus(int id)
    {
        var branch = context.Branches.Find(id);
        if (branch != null)
        {
            // Logique pour inverser l'état
            branch.Status = !branch.Status;
            context.SaveChanges();

            TempData["Message"] = "Modification réussie";
        }
        else
        {
            TempData["Error"] = "Erreur lors de la modification";
        }

        return RedirectToAction(nameof(Index));
    }
}
