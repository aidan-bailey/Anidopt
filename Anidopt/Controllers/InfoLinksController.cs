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
    public class InfoLinksController : Controller
    {
        private readonly AnidoptContext _context;

        public InfoLinksController(AnidoptContext context)
        {
            _context = context;
        }

        // GET: InfoLinks
        public async Task<IActionResult> Index()
        {
            var anidoptContext = _context.InfoLink.Include(i => i.Animal).Include(i => i.Descriptor);
            return View(await anidoptContext.ToListAsync());
        }

        // GET: InfoLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InfoLink == null)
            {
                return NotFound();
            }

            var infoLink = await _context.InfoLink
                .Include(i => i.Animal)
                .Include(i => i.Descriptor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (infoLink == null)
            {
                return NotFound();
            }

            return View(infoLink);
        }

        // GET: InfoLinks/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name");
            ViewData["DescriptorId"] = new SelectList(_context.Descriptor, "Id", "Name");
            ViewData["DetailId"] = new SelectList(_context.Detail, "Id", "Description");
            return View();
        }

        // POST: InfoLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalId,DescriptorId,DetailId")] InfoLink infoLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(infoLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name", infoLink.AnimalId);
            ViewData["DescriptorId"] = new SelectList(_context.Descriptor, "Id", "Name", infoLink.DescriptorId);
            return View(infoLink);
        }

        // GET: InfoLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InfoLink == null)
            {
                return NotFound();
            }

            var infoLink = await _context.InfoLink.FindAsync(id);
            if (infoLink == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name", infoLink.AnimalId);
            ViewData["DescriptorId"] = new SelectList(_context.Descriptor, "Id", "Name", infoLink.DescriptorId);
            return View(infoLink);
        }

        // POST: InfoLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnimalId,DescriptorId,DetailId")] InfoLink infoLink)
        {
            if (id != infoLink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(infoLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoLinkExists(infoLink.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name", infoLink.AnimalId);
            ViewData["DescriptorId"] = new SelectList(_context.Descriptor, "Id", "Name", infoLink.DescriptorId);
            return View(infoLink);
        }

        // GET: InfoLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InfoLink == null)
            {
                return NotFound();
            }

            var infoLink = await _context.InfoLink.Include(i => i.Animal).Include(i => i.Descriptor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (infoLink == null)
            {
                return NotFound();
            }

            return View(infoLink);
        }

        // POST: InfoLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InfoLink == null)
            {
                return Problem("Entity set 'AnidoptContext.InfoLink'  is null.");
            }
            var infoLink = await _context.InfoLink.FindAsync(id);
            if (infoLink != null)
            {
                _context.InfoLink.Remove(infoLink);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoLinkExists(int id)
        {
          return (_context.InfoLink?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
