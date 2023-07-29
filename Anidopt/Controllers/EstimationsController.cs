using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Anidopt.Controllers;

[Authorize(Roles = "SiteAdmin")]
public class EstimationsController : Controller {
    private readonly IEstimationService _estimationService;
    private readonly ISpeciesService _speciesService;
    private readonly IBreedService _breedService;
    private readonly ISexService _sexService;

    public EstimationsController(IEstimationService estimationService, ISpeciesService speciesService, IBreedService breedService, ISexService sexService) {
        _estimationService = estimationService;
        _speciesService = speciesService;
        _breedService = breedService;
        _sexService = sexService;
    }

    // GET: Estimations
    public async Task<IActionResult> Index() {
        return View(await _estimationService.GetAll().ToListAsync());
    }

    // GET: Estimations/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null || !_estimationService.Initialised)
            return NotFound();
        var estimation = await _estimationService.GetByIdAsync((int)id);
        if (estimation == null)
            return NotFound();
        return View(estimation);
    }

    // GET: Estimations/Create
    public async Task<IActionResult> Create() {
        ViewBag.Species = new SelectList(await _speciesService.GetAll().ToListAsync(), "Id", "Name");
        ViewBag.Breeds = new SelectList(await _breedService.GetAll().ToListAsync(), "Id", "Name");
        ViewBag.Sexes = new SelectList(await _sexService.GetAll().ToListAsync(), "Id", "Name");
        return View();
    }

    // POST: Estimations/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Height,Weight,BreedId,SexId")] Estimation estimation) {
        if (ModelState.IsValid) {
            await _estimationService.AddAsync(estimation);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Species = new SelectList(await _speciesService.GetAll().ToListAsync(), "Id", "Name", (int)estimation.Breed?.SpeciesId);
        ViewBag.Breeds = new SelectList(await _breedService.GetAll().ToListAsync(), "Id", "Name", estimation.BreedId);
        ViewBag.Sexes = new SelectList(await _sexService.GetAll().ToListAsync(), "Id", "Name", estimation.SexId);
        return View(estimation);
    }

    // GET: Estimations/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || !_estimationService.Initialised)
            return NotFound();

        var estimation = await _estimationService.GetByIdAsync((int)id);
        if (estimation == null)
            return NotFound();
        ViewBag.Species = new SelectList(await _speciesService.GetAll().ToListAsync(), "Id", "Name", (int)estimation.Breed?.SpeciesId);
        ViewBag.Breeds = new SelectList(await _breedService.GetAll().ToListAsync(), "Id", "Name", estimation.BreedId);
        ViewBag.Sexes = new SelectList(await _sexService.GetAll().ToListAsync(), "Id", "Name", estimation.SexId);
        return View(estimation);
    }

    // POST: Estimations/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Height,Weight,BreedId,SexId")] Estimation estimation) {
        if (id != estimation.Id)
            return NotFound();
        if (ModelState.IsValid) {
            try {
                await _estimationService.UpdateAsync(estimation);
            } catch (DbUpdateConcurrencyException) {
                if (!_estimationService.ExistsById(estimation.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Species = new SelectList(await _speciesService.GetAll().ToListAsync(), "Id", "Name", (int)estimation.Breed?.SpeciesId);
        ViewBag.Breeds = new SelectList(await _breedService.GetAll().ToListAsync(), "Id", "Name", estimation.BreedId);
        ViewBag.Sexes = new SelectList(await _sexService.GetAll().ToListAsync(), "Id", "Name", estimation.SexId);
        return View(estimation);
    }

    // GET: Estimations/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null || !_estimationService.Initialised)
            return NotFound();
        var estimation = await _estimationService.GetByIdAsync((int)id);
        if (estimation == null)
            return NotFound();
        return View(estimation);
    }

    // POST: Estimations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        if (!_estimationService.Initialised)
            return Problem("Entity set 'AnidoptContext.Estimation'  is null.");
        await _estimationService.EnsureDeletionByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
