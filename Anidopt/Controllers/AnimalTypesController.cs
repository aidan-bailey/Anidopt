using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anidopt.Data;
using Anidopt.Models;

namespace Anidopt.Controllers
{
    public class AnimalTypesController : Controller
    {
        private readonly AnidoptContext _context;

        public AnimalTypesController(AnidoptContext context)
        {
            _context = context;
        }

        // GET: AnimalTypes
        public async Task<IActionResult> Index()
        {
              return _context.AnimalType != null ? 
                          View(await _context.AnimalType.ToListAsync()) :
                          Problem("Entity set 'AnidoptContext.AnimalType'  is null.");
        }

        // GET: AnimalTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimalType == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalType == null)
            {
                return NotFound();
            }

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
            if (id == null || _context.AnimalType == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalType.FindAsync(id);
            if (animalType == null)
            {
                return NotFound();
            }
            return View(animalType);
        }

        // POST: AnimalTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AnimalType animalType)
        {
            if (id != animalType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalTypeExists(animalType.Id))
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
            return View(animalType);
        }

        // GET: AnimalTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnimalType == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalType == null)
            {
                return NotFound();
            }

            return View(animalType);
        }

        // POST: AnimalTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnimalType == null)
            {
                return Problem("Entity set 'AnidoptContext.AnimalType'  is null.");
            }
            var animalType = await _context.AnimalType.FindAsync(id);
            if (animalType != null)
            {
                _context.AnimalType.Remove(animalType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalTypeExists(int id)
        {
          return (_context.AnimalType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
