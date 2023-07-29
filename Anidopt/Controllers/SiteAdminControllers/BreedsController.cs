using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers.SiteAdminControllers;

[Authorize(Roles = "SiteAdmin")]
public class BreedsController : Controller
{
    private readonly IBreedService _breedService;
    private readonly ISpeciesService _speciesService;

    private string ViewPath(string name) => "~/Views/SiteAdmin/Breeds/" + name + ".cshtml";

    public BreedsController(IBreedService breedService, ISpeciesService speciesService)
    {
        _breedService = breedService;
        _speciesService = speciesService;
    }

    // GET: Breeds
    public async Task<IActionResult> Index()
    {
        if (!_breedService.Initialised) return Problem("Entity set 'AnidoptContext.Breed'  is null.");
        return View(ViewPath("Index"), await _breedService.GetAll().ToListAsync());
    }

    // GET: Breeds/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || !_breedService.Initialised) return NotFound();
        var breed = await _breedService.GetByIdAsync((int)id);
        if (breed == null) return NotFound();
        return View(ViewPath("Details"), breed);
    }

    // GET: Breeds/Create
    public async Task<IActionResult> Create()
    {
        var speciess = await _speciesService.GetAll().ToListAsync();
        ViewBag.Speciess = new SelectList(speciess, "Id", "Name");
        return View(ViewPath("Create"));
    }

    // GET: Breeds/ForSpecies
    public async Task<IActionResult> ForSpecies(int? id)
    {
        if (id == null) return NotFound();
        var breeds = await _breedService.GetForSpeciesByIdAsync((int)id);
        return Ok(breeds);
    }

    // POST: Breeds/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,SpeciesId")] Breed breed)
    {
        if (ModelState.IsValid)
        {
            await _breedService.AddAsync(breed);
            return RedirectToAction(nameof(Index));
        }
        var speciess = await _speciesService.GetAll().ToListAsync();
        ViewBag.Speciess = new SelectList(speciess, "Id", "Name", breed.SpeciesId);
        return View(ViewPath("Create"), breed);
    }

    // GET: Breeds/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || !_breedService.Initialised) return NotFound();
        var breed = await _breedService.GetByIdAsync((int)id);
        if (breed == null) return NotFound();
        var speciess = await _speciesService.GetAll().ToListAsync();
        ViewBag.Speciess = new SelectList(speciess, "Id", "Name", breed.SpeciesId);
        return View(ViewPath("Edit"), breed);
    }

    // POST: Breeds/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Breed breed)
    {
        if (id != breed.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                await _breedService.UpdateAsync(breed);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_breedService.ExistsById(breed.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        var speciess = await _speciesService.GetAll().ToListAsync();
        ViewBag.Speciess = new SelectList(speciess, "Id", "Name", breed.SpeciesId);
        return View(ViewPath("Edit"), breed);
    }

    // GET: Breeds/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || !_breedService.Initialised) return NotFound();
        var breed = await _breedService.GetByIdAsync((int)id);
        if (breed == null) return NotFound();
        return View(ViewPath("Delete"), breed);
    }

    // POST: Breeds/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!_breedService.Initialised) return Problem("Entity set 'AnidoptContext.Breed'  is null.");
        await _breedService.EnsureDeletionByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
