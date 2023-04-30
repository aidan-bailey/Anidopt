using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers;

public class AnimalsController : Controller
{
    private readonly IAnimalService _animalService;
    private readonly IBreedService _breedService;
    private readonly ISpeciesService _speciesService;
    private readonly IOrganisationService _organisationService;
    private readonly ISexService _sexService;

    public AnimalsController(IAnimalService animalService, ISpeciesService speciesService, IBreedService breedService, IOrganisationService organisationService, ISexService sexService)
    {
        _animalService = animalService;
        _breedService = breedService;
        _speciesService = speciesService;
        _organisationService = organisationService;
        _sexService = sexService;
    }

    // GET: Animals
    public async Task<IActionResult> Index()
    {
        if (!_animalService.Initialised) Problem("Entity set 'AnidoptContext.Animal'  is null.");
        var animals = await _animalService.GetAllAsync();
        return View(animals);
    }

    // GET: Animals/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || !_animalService.Initialised) return NotFound();
        var animal = await _animalService.GetByIdAsync((int)id);
        if (animal == null) return NotFound();
        return View(animal);
    }

    // GET: Animals/Create
    public async Task<IActionResult> Create()
    {
        var species = await _speciesService.GetAllAsync();
        ViewBag.Species = new SelectList(species, "Id", "Name");
        var breeds = await _breedService.GetAllAsync();
        ViewBag.Breeds = new SelectList(breeds, "Id", "Name");
        var organisations = await _organisationService.GetAllAsync();
        ViewBag.Organisations = new SelectList(organisations, "Id", "Name");
        var sexes = await _sexService.GetAllAsync();
        ViewBag.Sexes = new SelectList(sexes, "Id", "Name");
        return View();
    }

    // POST: Animals/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Age,SpeciesId,OrganisationId,SexId,Description")] Animal animal)
    {
        if (ModelState.IsValid)
        {
            await _animalService.AddAsync(animal);
            return RedirectToAction(nameof(Index));
        }
        var organisations = await _organisationService.GetAllAsync();
        ViewBag.Organisations = new SelectList(organisations, "Id", "Name", animal.OrganisationId);
        var species = await _speciesService.GetAllAsync();
        ViewBag.Species = new SelectList(species, "Id", "Name", (int)animal.Breed?.SpeciesId);
        var breeds = await _breedService.GetAllAsync();
        ViewBag.Breeds = new SelectList(breeds, "Id", "Name", animal.BreedId);
        var sexes = await _sexService.GetAllAsync();
        ViewBag.Sexes = new SelectList(sexes, "Id", "Name", animal.SexId);
        return View(animal);
    }

    // GET: Animals/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || !_animalService.Initialised) return NotFound();
        var animal = await _animalService.GetByIdAsync((int)id);
        if (animal == null) return NotFound();
        var organisations = await _organisationService.GetAllAsync();
        ViewBag.Organisations = new SelectList(organisations, "Id", "Name", animal.OrganisationId);
        var species = await _speciesService.GetAllAsync();
        ViewBag.Species = new SelectList(species, "Id", "Name", animal.Breed?.SpeciesId);
        var breeds = await _breedService.GetForSpeciesByIdAsync((int) animal.Breed?.SpeciesId);
        ViewBag.Breeds = new SelectList(breeds, "Id", "Name", animal.BreedId);
        var sexes = await _sexService.GetAllAsync();
        ViewBag.Sexes = new SelectList(sexes, "Id", "Name", animal.SexId);
        return View(animal);
    }

    // POST: Animals/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,SpeciesId,BreedId,SexId,OrganisationId,Description")] Animal animal)
    {
        if (id != animal.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                await _animalService.UpdateAsync(animal);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await _animalService.ExistsByIdAsync(animal.Id))) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        var organisations = await _organisationService.GetAllAsync();
        ViewBag.Organisations = new SelectList(organisations, "Id", "Name", animal.OrganisationId);
        var species = await _speciesService.GetAllAsync();
        ViewBag.Species = new SelectList(species, "Id", "Name", animal.Breed?.SpeciesId);
        var breeds = await _breedService.GetForSpeciesByIdAsync((int) animal.Breed?.SpeciesId);
        ViewBag.Breeds = new SelectList(breeds, "Id", "Name", animal.BreedId);
        var sexes = await _sexService.GetAllAsync();
        ViewBag.Sexes = new SelectList(sexes, "Id", "Name", animal.SexId);
        return View(animal);
    }

    // GET: Animals/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || !_animalService.Initialised) return NotFound();
        var animal = await _animalService.GetByIdAsync((int)id);
        if (animal == null) return NotFound();
        return View(animal);
    }

    // POST: Animals/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!_animalService.Initialised) return Problem("Entity set 'AnidoptContext.Animal'  is null.");
        await _animalService.EnsureDeletionByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
