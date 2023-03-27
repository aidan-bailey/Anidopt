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
    public class SpeciesController : Controller
    {
        private readonly AnidoptContext _context;
        private readonly ISpeciesService _SpeciesService;

        public SpeciesController(AnidoptContext context, ISpeciesService SpeciesService)
        {
            _context = context;
            _SpeciesService = SpeciesService;
        }

        // GET: Speciess
        public async Task<IActionResult> Index()
        {
            if (!_SpeciesService.Initialised) return Problem("Entity set 'AnidoptContext.Species'  is null.");
            var Speciess = await _SpeciesService.GetSpeciesAsync();
            return View(Speciess);
        }

        // GET: Speciess/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_SpeciesService.Initialised) return NotFound();
            var Species = await _SpeciesService.GetSpeciesByIdAsync((int)id);
            if (Species == null) return NotFound();
            return View(Species);
        }

        // GET: Speciess/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speciess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Species Species)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Species);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Species);
        }

        // GET: Speciess/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_SpeciesService.Initialised) return NotFound();
            var Species = await _SpeciesService.GetSpeciesByIdAsync((int)id);
            if (Species == null) return NotFound();
            return View(Species);
        }

        // POST: Speciess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Species Species)
        {
            if (id != Species.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Species);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_SpeciesService.GetSpeciesExists(Species.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Species);
        }

        // GET: Speciess/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_SpeciesService.Initialised) return NotFound();
            var Species = await _SpeciesService.GetSpeciesByIdAsync((int)id);
            if (Species == null) return NotFound();
            return View(Species);
        }

        // POST: Speciess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_SpeciesService.Initialised) return Problem("Entity set 'AnidoptContext.Species'  is null.");
            await _SpeciesService.EnsureSpeciesDeletionById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
