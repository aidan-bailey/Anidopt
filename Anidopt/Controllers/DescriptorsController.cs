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
    public class DescriptorsController : Controller
    {
        private readonly AnidoptContext _context;

        public DescriptorsController(AnidoptContext context)
        {
            _context = context;
        }

        // GET: Descriptors
        public async Task<IActionResult> Index()
        {
            var anidoptContext = _context.Descriptor.Include(d => d.DescriptorType);
            return View(await anidoptContext.ToListAsync());
        }

        // GET: Descriptors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Descriptor == null)
            {
                return NotFound();
            }

            var descriptor = await _context.Descriptor
                .Include(d => d.DescriptorType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descriptor == null)
            {
                return NotFound();
            }

            return View(descriptor);
        }

        // GET: Descriptors/Create
        public IActionResult Create()
        {
            ViewData["DescriptorTypeId"] = new SelectList(_context.DescriptorType, "Id", "Name");
            return View();
        }

        // POST: Descriptors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DescriptorTypeId,Name")] Descriptor descriptor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(descriptor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DescriptorTypeId"] = new SelectList(_context.DescriptorType, "Id", "Name", descriptor.DescriptorTypeId);
            return View(descriptor);
        }

        // GET: Descriptors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Descriptor == null)
            {
                return NotFound();
            }

            var descriptor = await _context.Descriptor.FindAsync(id);
            if (descriptor == null)
            {
                return NotFound();
            }
            ViewData["DescriptorTypeId"] = new SelectList(_context.DescriptorType, "Id", "Name", descriptor.DescriptorTypeId);
            return View(descriptor);
        }

        // POST: Descriptors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DescriptorTypeId,Name")] Descriptor descriptor)
        {
            if (id != descriptor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(descriptor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescriptorExists(descriptor.Id))
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
            ViewData["DescriptorTypeId"] = new SelectList(_context.DescriptorType, "Id", "Name", descriptor.DescriptorTypeId);
            return View(descriptor);
        }

        // GET: Descriptors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Descriptor == null)
            {
                return NotFound();
            }

            var descriptor = await _context.Descriptor
                .Include(d => d.DescriptorType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descriptor == null)
            {
                return NotFound();
            }

            return View(descriptor);
        }

        // POST: Descriptors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Descriptor == null)
            {
                return Problem("Entity set 'AnidoptContext.Descriptor'  is null.");
            }
            var descriptor = await _context.Descriptor.FindAsync(id);
            if (descriptor != null)
            {
                _context.Descriptor.Remove(descriptor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescriptorExists(int id)
        {
          return (_context.Descriptor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
