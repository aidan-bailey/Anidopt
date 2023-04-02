using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers
{
    public class BreedsController : Controller
    {
        private readonly AnidoptContext _context;
        private readonly IBreedService _breedService;
        private readonly ISpeciesService _SpeciesService;

        public BreedsController(AnidoptContext context, IBreedService breedService, ISpeciesService SpeciesService)
        {
            _context = context;
            _breedService = breedService;
            _SpeciesService = SpeciesService;
        }

        // GET: Breeds
        public async Task<IActionResult> Index()
        {
            if (!_breedService.Initialised) return Problem("Entity set 'AnidoptContext.Breed'  is null.");
            return View(await _breedService.GetBreedsAsync());
        }

        // GET: Breeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_breedService.Initialised) return NotFound();
            var breed = await _breedService.GetBreedByIdAsync((int)id);
            if (breed == null) return NotFound();
            return View(breed);
        }

        // GET: Breeds/Create
        public async Task<IActionResult> Create()
        {
            var Speciess = await _SpeciesService.GetSpeciesAsync();
            ViewBag.Speciess = Speciess.Select(at => new SelectListItem
            {
                Text = at.Name,
                Value = at.Id.ToString()
            });
            return View();
        }

        // GET: Breeds/ForSpecies
        public async Task<IActionResult> ForSpecies(int? id)
        {
            if (id == null) return NotFound();
            var breeds = await _breedService.GetBreedsForSpeciesByIdAsync((int)id);
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
                _context.Add(breed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var Speciess = await _SpeciesService.GetSpeciesAsync();
            ViewBag.Speciess = Speciess.Select(at => new SelectListItem
            {
                Text = at.Name,
                Value = at.Id.ToString(),
                Selected = breed.SpeciesId == at.Id
            });
            return View(breed);
        }

        // GET: Breeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_breedService.Initialised) return NotFound();
            var breed = await _breedService.GetBreedByIdAsync((int)id);
            if (breed == null) return NotFound();
            var Speciess = await _SpeciesService.GetSpeciesAsync();
            ViewBag.Speciess = Speciess.Select(at => new SelectListItem
            {
                Text = at.Name,
                Value = at.Id.ToString()
            });
            return View(breed);
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
                    _context.Update(breed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_breedService.BreedExistsById(breed.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var Speciess = await _SpeciesService.GetSpeciesAsync();
            ViewBag.Speciess = Speciess.Select(at => new SelectListItem
            {
                Text = at.Name,
                Value = at.Id.ToString(),
                Selected = breed.SpeciesId == at.Id
            });
            return View(breed);
        }

        // GET: Breeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_breedService.Initialised) return NotFound();
            var breed = await _breedService.GetBreedByIdAsync((int)id);
            if (breed == null) return NotFound();
            return View(breed);
        }

        // POST: Breeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_breedService.Initialised) return Problem("Entity set 'AnidoptContext.Breed'  is null.");
            await _breedService.EnsureBreedDeletionById((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}
