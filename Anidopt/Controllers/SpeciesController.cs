using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers
{
    public class SpeciesController : Controller
    {
        private readonly ISpeciesService _speciesService;

        public SpeciesController(ISpeciesService SpeciesService)
        {
            _speciesService = SpeciesService;
        }

        // GET: Speciess
        public async Task<IActionResult> Index()
        {
            if (!_speciesService.Initialised) return Problem("Entity set 'AnidoptContext.Species'  is null.");
            var speciess = await _speciesService.GetAllAsync();
            return View(speciess);
        }

        // GET: Speciess/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_speciesService.Initialised) return NotFound();
            var species = await _speciesService.GetByIdAsync((int)id);
            if (species == null) return NotFound();
            return View(species);
        }

        // GET: Speciess/Create
        public IActionResult Create() => View();

        // POST: Speciess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Species species)
        {
            if (ModelState.IsValid)
            {
                await _speciesService.AddAsync(species);
                return RedirectToAction(nameof(Index));
            }
            return View(species);
        }

        // GET: Speciess/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_speciesService.Initialised) return NotFound();
            var species = await _speciesService.GetByIdAsync((int)id);
            if (species == null) return NotFound();
            return View(species);
        }

        // POST: Speciess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Species species)
        {
            if (id != species.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await _speciesService.UpdateAsync(species);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_speciesService.ExistsById(species.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(species);
        }

        // GET: Speciess/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_speciesService.Initialised) return NotFound();
            var species = await _speciesService.GetByIdAsync((int)id);
            if (species == null) return NotFound();
            return View(species);
        }

        // POST: Speciess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_speciesService.Initialised) return Problem("Entity set 'AnidoptContext.Species'  is null.");
            await _speciesService.EnsureDeletionByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
