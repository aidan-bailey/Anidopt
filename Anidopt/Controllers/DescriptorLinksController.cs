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
    public class DescriptorLinksController : Controller
    {
        private readonly AnidoptContext _context;

        public DescriptorLinksController(AnidoptContext context)
        {
            _context = context;
        }

        // GET: DescriptorLinks
        public async Task<IActionResult> Index()
        {
            var anidoptContext = _context.DescriptorLink.Include(i => i.Animal).Include(i => i.Descriptor);
            return View(await anidoptContext.ToListAsync());
        }

        // GET: DescriptorLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DescriptorLink == null)
            {
                return NotFound();
            }

            var DescriptorLink = await _context.DescriptorLink
                .Include(i => i.Animal)
                .Include(i => i.Descriptor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (DescriptorLink == null)
            {
                return NotFound();
            }

            return View(DescriptorLink);
        }

        // GET: DescriptorLinks/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name");
            ViewData["DescriptorId"] = new SelectList(_context.Descriptor, "Id", "Name");
            return View();
        }

        // POST: DescriptorLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalId,DescriptorId,DetailId")] DescriptorLink DescriptorLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(DescriptorLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name", DescriptorLink.AnimalId);
            ViewData["DescriptorId"] = new SelectList(_context.Descriptor, "Id", "Name", DescriptorLink.DescriptorId);
            return View(DescriptorLink);
        }

        // GET: DescriptorLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DescriptorLink == null)
            {
                return NotFound();
            }

            var DescriptorLink = await _context.DescriptorLink.FindAsync(id);
            if (DescriptorLink == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name", DescriptorLink.AnimalId);
            ViewData["DescriptorId"] = new SelectList(_context.Descriptor, "Id", "Name", DescriptorLink.DescriptorId);
            return View(DescriptorLink);
        }

        // POST: DescriptorLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnimalId,DescriptorId,DetailId")] DescriptorLink DescriptorLink)
        {
            if (id != DescriptorLink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(DescriptorLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescriptorLinkExists(DescriptorLink.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Name", DescriptorLink.AnimalId);
            ViewData["DescriptorId"] = new SelectList(_context.Descriptor, "Id", "Name", DescriptorLink.DescriptorId);
            return View(DescriptorLink);
        }

        // GET: DescriptorLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DescriptorLink == null)
            {
                return NotFound();
            }

            var DescriptorLink = await _context.DescriptorLink.Include(i => i.Animal).Include(i => i.Descriptor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (DescriptorLink == null)
            {
                return NotFound();
            }

            return View(DescriptorLink);
        }

        // POST: DescriptorLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DescriptorLink == null)
            {
                return Problem("Entity set 'AnidoptContext.DescriptorLink'  is null.");
            }
            var DescriptorLink = await _context.DescriptorLink.FindAsync(id);
            if (DescriptorLink != null)
            {
                _context.DescriptorLink.Remove(DescriptorLink);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescriptorLinkExists(int id)
        {
          return (_context.DescriptorLink?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
