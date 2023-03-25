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
    public class AnimalTypesController : Controller
    {
        private readonly AnidoptContext _context;
        private readonly IAnimalTypeService _animalTypeService;

        public AnimalTypesController(AnidoptContext context, IAnimalTypeService animalTypeService)
        {
            _context = context;
            _animalTypeService = animalTypeService;
        }

        // GET: AnimalTypes
        public async Task<IActionResult> Index()
        {
            if (_context.AnimalType == null) return Problem("Entity set 'AnidoptContext.AnimalType'  is null.");
            var animalTypes = await _animalTypeService.GetAnimalTypesAsync();
            return View(animalTypes);
        }

        // GET: AnimalTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimalType == null) return NotFound();
            var animalType = await _animalTypeService.GetAnimalTypeByIdAsync((int)id);
            if (animalType == null) return NotFound();
            return View(animalType);
        }

        // GET: AnimalTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimalTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AnimalType animalType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animalType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalType);
        }

        // GET: AnimalTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnimalType == null) return NotFound();
            var animalType = await _animalTypeService.GetAnimalTypeByIdAsync((int)id);
            if (animalType == null) return NotFound();
            return View(animalType);
        }

        // POST: AnimalTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AnimalType animalType)
        {
            if (id != animalType.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_animalTypeService.GetAnimalTypeExists(animalType.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(animalType);
        }

        // GET: AnimalTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnimalType == null) return NotFound();
            var animalType = await _animalTypeService.GetAnimalTypeByIdAsync((int)id);
            if (animalType == null) return NotFound();
            return View(animalType);
        }

        // POST: AnimalTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnimalType == null) return Problem("Entity set 'AnidoptContext.AnimalType'  is null.");
            var animalType = await _animalTypeService.GetAnimalTypeByIdAsync(id);
            if (animalType != null) _context.AnimalType.Remove(animalType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
