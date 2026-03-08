using LocationManageCore.Data;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1.Models.Branchs;


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
            .Include(b=> b.Cars)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (branch == null)
        {
            return NotFound();
        }

        return View(branch.Cars.ToList()); // c'est ici que le type est passé en paramètre
    }


}
