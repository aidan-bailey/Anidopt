using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Anidopt.Controllers.SiteAdminControllers;

[Authorize(Roles = "SiteAdmin")]
public class SexesController : Controller
{
    private readonly ISexService _sexService;

    private string ViewPath(string name) => "~/Views/SiteAdmin/Sexes/" + name + ".cshtml";

    public SexesController(ISexService sexService)
    {
        _sexService = sexService;
    }

    // GET: Sexes
    public async Task<IActionResult> Index() => _sexService.Initialised
        ? View(await _sexService.GetAll().ToListAsync())
        : Problem("Entity set 'AnidoptContext.Sex'  is null.");

    // GET: Sexes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || !_sexService.Initialised) return NotFound();
        var sex = await _sexService.GetByIdAsync((int)id);
        if (sex == null) return NotFound();
        return View(ViewPath("Details"), sex);
    }

    // GET: Sexes/Create
    public IActionResult Create() => View();

    // POST: Sexes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Sex sex)
    {
        if (ModelState.IsValid)
        {
            await _sexService.AddAsync(sex);
            return RedirectToAction(nameof(Index));
        }
        return View(ViewPath("Create"), sex);
    }

    // GET: Sexes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || !_sexService.Initialised) return NotFound();
        var sex = await _sexService.GetByIdAsync((int)id);
        if (sex == null) return NotFound();
        return View(ViewPath("Edit"), sex);
    }

    // POST: Sexes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Sex sex)
    {
        if (id != sex.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                await _sexService.UpdateAsync(sex);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _sexService.ExistsByIdAsync(sex.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(ViewPath("Edit"), sex);
    }

    // GET: Sexes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || !_sexService.Initialised) return NotFound();
        var sex = await _sexService.GetByIdAsync((int)id);
        if (sex == null) return NotFound();
        return View(ViewPath("Delete"), sex);
    }

    // POST: Sexes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!_sexService.Initialised) return Problem("Entity set 'AnidoptContext.Sex'  is null.");
        await _sexService.EnsureDeletionByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
