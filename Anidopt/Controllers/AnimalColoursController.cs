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
    public class AnimalColoursController : Controller
    {
        private readonly AnidoptContext _context;

        public AnimalColoursController(AnidoptContext context)
        {
            _context = context;
        }

        // GET: AnimalColours
        public async Task<IActionResult> Index()
        {
              return _context.AnimalColour != null ? 
                          View(await _context.AnimalColour.ToListAsync()) :
                          Problem("Entity set 'AnidoptContext.AnimalColour'  is null.");
        }

        // GET: AnimalColours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimalColour == null)
            {
                return NotFound();
            }

            var animalColour = await _context.AnimalColour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalColour == null)
            {
                return NotFound();
            }

            return View(animalColour);
        }

        // GET: AnimalColours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimalColours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Colour,Id")] AnimalColour animalColour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animalColour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalColour);
        }

        // GET: AnimalColours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnimalColour == null)
            {
                return NotFound();
            }

            var animalColour = await _context.AnimalColour.FindAsync(id);
            if (animalColour == null)
            {
                return NotFound();
            }
            return View(animalColour);
        }

        // POST: AnimalColours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Colour,Id")] AnimalColour animalColour)
        {
            if (id != animalColour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalColour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalColourExists(animalColour.Id))
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
            return View(animalColour);
        }

        // GET: AnimalColours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnimalColour == null)
            {
                return NotFound();
            }

            var animalColour = await _context.AnimalColour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalColour == null)
            {
                return NotFound();
            }

            return View(animalColour);
        }

        // POST: AnimalColours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnimalColour == null)
            {
                return Problem("Entity set 'AnidoptContext.AnimalColour'  is null.");
            }
            var animalColour = await _context.AnimalColour.FindAsync(id);
            if (animalColour != null)
            {
                _context.AnimalColour.Remove(animalColour);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalColourExists(int id)
        {
          return (_context.AnimalColour?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
