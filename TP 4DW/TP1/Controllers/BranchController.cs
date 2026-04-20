using LocationManageCore.Data;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1.Constantes;
using TP1.Models.Branches;

namespace TP1.Controllers;

[Authorize(Roles = Roles.ADMIN)]


public class BranchController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext Context = context;


    [HttpGet]
    public IActionResult Index()
    {
        return RedirectToAction(nameof(List));
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var branches = await Context.Branches
            .Include(b => b.Cars)
            .Select(b => new BranchItem
            {
                BranchId = b.BranchId,
                Name = b.Name,
                Status = b.Status,
                ActiveCarsCount = b.Cars.Count(c => c.Status == "Actif"),
                DisabledCarsCount = b.Cars.Count(c => c.Status == "Désactivé")
            })
            .ToListAsync();

        return View(branches);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = Roles.ADMIN)]
    public async Task<IActionResult> Create(BranchCreate model)
    {
        if (!ModelState.IsValid) return View(model);

        var branch = Branch.Create(model.Status, model.Name);

        Context.Branches.Add(branch);
        await Context.SaveChangesAsync();

        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid branchId)
    {
        var branch = await Context.Branches.FindAsync(branchId);
        if (branch is null) return NotFound();

        var model = new BranchEdit
        {
            Id = branch.BranchId,
            Status = branch.Status,
            Name = branch.Name
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BranchEdit model)
    {
        if (!ModelState.IsValid) return View(model);

        var branch = await Context.Branches.FindAsync(model.Id);
        if (branch is null) return NotFound();

        branch.Status = model.Status;
        branch.Name = model.Name;

        await Context.SaveChangesAsync();

        string action = model.Status ? "activée" : "désactivée";
        TempData["Message"] = $"La succursale {branch.Name} a été {action} avec succès.";

        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid branchId)
    {
        var branch = await Context.Branches.FindAsync(branchId);
        if (branch is null) return NotFound();

        if (await Context.Cars.AnyAsync(c => c.BranchId == branchId))
        {
            TempData["Erreur"] = "Impossible de supprimer une succursale qui contient des véhicules.";
            return RedirectToAction(nameof(List));
        }

        Context.Branches.Remove(branch);
        await Context.SaveChangesAsync();

        TempData["Message"] = "Succursale supprimée avec succès.";
        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    public async Task<IActionResult> ToggleStatus(Guid branchId)
    {
        var branch = await Context.Branches.FindAsync(branchId);
        if (branch != null)
        {
            branch.Status = !branch.Status;
            await Context.SaveChangesAsync();
            TempData["Message"] = "Statut mis à jour.";
        }

        return RedirectToAction(nameof(List));
    }
}