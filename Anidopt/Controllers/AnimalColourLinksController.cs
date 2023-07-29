using Anidopt.Data;
using Anidopt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers;

public class AnimalColourLinksController : Controller {
    private readonly AnidoptContext _context;

    public AnimalColourLinksController(AnidoptContext context) {
        _context = context;
    }

    // GET: AnimalColourLinks
    public async Task<IActionResult> Index() {
        var anidoptContext = _context.AnimalColourLink.Include(a => a.Animal).Include(a => a.Colour);
        return View(await anidoptContext.ToListAsync());
    }

    // GET: AnimalColourLinks/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null || _context.AnimalColourLink == null)             return NotFound();

        var animalColourLink = await _context.AnimalColourLink
            .Include(a => a.Animal)
            .Include(a => a.Colour)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (animalColourLink == null)             return NotFound();

        return View(animalColourLink);
    }

    // GET: AnimalColourLinks/Create
    public IActionResult Create() {
        ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name");
        ViewData["ColourId"] = new SelectList(_context.AnimalColour, "Id", "Colour");
        return View();
    }

    // POST: AnimalColourLinks/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AnimalId,ColourId,Id")] AnimalColourLink animalColourLink) {
        if (ModelState.IsValid) {
            _context.Add(animalColourLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name", animalColourLink.AnimalId);
        ViewData["ColourId"] = new SelectList(_context.AnimalColour, "Id", "Colour", animalColourLink.ColourId);
        return View(animalColourLink);
    }

    // GET: AnimalColourLinks/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.AnimalColourLink == null)             return NotFound();

        var animalColourLink = await _context.AnimalColourLink.FindAsync(id);
        if (animalColourLink == null)             return NotFound();
        ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name", animalColourLink.AnimalId);
        ViewData["ColourId"] = new SelectList(_context.AnimalColour, "Id", "Colour", animalColourLink.ColourId);
        return View(animalColourLink);
    }

    // POST: AnimalColourLinks/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AnimalId,ColourId,Id")] AnimalColourLink animalColourLink) {
        if (id != animalColourLink.Id)             return NotFound();

        if (ModelState.IsValid) {
            try {
                _context.Update(animalColourLink);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!AnimalColourLinkExists(animalColourLink.Id))                     return NotFound();
else                     throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name", animalColourLink.AnimalId);
        ViewData["ColourId"] = new SelectList(_context.AnimalColour, "Id", "Colour", animalColourLink.ColourId);
        return View(animalColourLink);
    }

    // GET: AnimalColourLinks/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null || _context.AnimalColourLink == null)             return NotFound();

        var animalColourLink = await _context.AnimalColourLink
            .Include(a => a.Animal)
            .Include(a => a.Colour)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (animalColourLink == null)             return NotFound();

        return View(animalColourLink);
    }

    // POST: AnimalColourLinks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        if (_context.AnimalColourLink == null)             return Problem("Entity set 'AnidoptContext.AnimalColourLink'  is null.");
        var animalColourLink = await _context.AnimalColourLink.FindAsync(id);
        if (animalColourLink != null)             _context.AnimalColourLink.Remove(animalColourLink);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AnimalColourLinkExists(int id) {
        return (_context.AnimalColourLink?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
