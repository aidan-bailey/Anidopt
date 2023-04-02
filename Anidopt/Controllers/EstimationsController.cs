using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Controllers
{
    public class EstimationsController : Controller
    {
        private readonly AnidoptContext _context;
        private readonly ISpeciesService _speciesService;
        private readonly IBreedService _breedService;
        private readonly ISexService _sexService;

        public EstimationsController(AnidoptContext context, ISpeciesService speciesService, IBreedService breedService, ISexService sexService)
        {
            _context = context;
            _speciesService = speciesService;
            _breedService = breedService;
            _sexService = sexService;
        }

        // GET: Estimations
        public async Task<IActionResult> Index()
        {
            var anidoptContext = _context.Estimation.Include(e => e.Breed).Include(e => e.Sex);
            return View(await anidoptContext.ToListAsync());
        }

        // GET: Estimations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estimation == null)
            {
                return NotFound();
            }

            var estimation = await _context.Estimation
                .Include(e => e.Breed)
                .Include(e => e.Sex)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estimation == null)
            {
                return NotFound();
            }

            return View(estimation);
        }

        // GET: Estimations/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Species = new SelectList(await _speciesService.GetSpeciesAsync(), "Id", "Name");
            ViewBag.Breeds = new SelectList(await _breedService.GetBreedsAsync(), "Id", "Name");
            ViewBag.Sexes = new SelectList(await _sexService.GetSexAsync(), "Id", "Name");
            return View();
        }

        // POST: Estimations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Height,Weight,BreedId,SexId")] Estimation estimation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estimation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Species = new SelectList(await _speciesService.GetSpeciesAsync(), "Id", "Name", (int)estimation.Breed?.SpeciesId);
            ViewBag.Breeds = new SelectList(await _breedService.GetBreedsAsync(), "Id", "Name", estimation.BreedId);
            ViewBag.Sexes = new SelectList(await _sexService.GetSexAsync(), "Id", "Name", estimation.SexId);
            return View(estimation);
        }

        // GET: Estimations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estimation == null)
            {
                return NotFound();
            }

            var estimation = await _context.Estimation.FindAsync(id);
            if (estimation == null)
            {
                return NotFound();
            }
            ViewBag.Species = new SelectList(await _speciesService.GetSpeciesAsync(), "Id", "Name", (int)estimation.Breed?.SpeciesId);
            ViewBag.Breeds = new SelectList(await _breedService.GetBreedsAsync(), "Id", "Name", estimation.BreedId);
            ViewBag.Sexes = new SelectList(await _sexService.GetSexAsync(), "Id", "Name", estimation.SexId);
            return View(estimation);
        }

        // POST: Estimations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Height,Weight,BreedId,SexId")] Estimation estimation)
        {
            if (id != estimation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estimation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstimationExists(estimation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Species = new SelectList(await _speciesService.GetSpeciesAsync(), "Id", "Name", (int)estimation.Breed?.SpeciesId);
            ViewBag.Breeds = new SelectList(await _breedService.GetBreedsAsync(), "Id", "Name", estimation.BreedId);
            ViewBag.Sexes = new SelectList(await _sexService.GetSexAsync(), "Id", "Name", estimation.SexId);
            return View(estimation);
        }

        // GET: Estimations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estimation == null)
            {
                return NotFound();
            }

            var estimation = await _context.Estimation
                .Include(e => e.Breed)
                .Include(e => e.Sex)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estimation == null)
            {
                return NotFound();
            }

            return View(estimation);
        }

        // POST: Estimations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estimation == null)
            {
                return Problem("Entity set 'AnidoptContext.Estimation'  is null.");
            }
            var estimation = await _context.Estimation.FindAsync(id);
            if (estimation != null)
            {
                _context.Estimation.Remove(estimation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstimationExists(int id)
        {
          return (_context.Estimation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
