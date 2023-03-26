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
    public class DescriptorTypesController : Controller
    {
        private readonly AnidoptContext _context;

        public DescriptorTypesController(AnidoptContext context)
        {
            _context = context;
        }

        // GET: DescriptorTypes
        public async Task<IActionResult> Index()
        {
              return _context.DescriptorType != null ? 
                          View(await _context.DescriptorType.ToListAsync()) :
                          Problem("Entity set 'AnidoptContext.DescriptorType'  is null.");
        }

        // GET: DescriptorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DescriptorType == null)
            {
                return NotFound();
            }

            var descriptorType = await _context.DescriptorType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descriptorType == null)
            {
                return NotFound();
            }

            return View(descriptorType);
        }

        // GET: DescriptorTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DescriptorTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DescriptorType descriptorType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(descriptorType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(descriptorType);
        }

        // GET: DescriptorTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DescriptorType == null)
            {
                return NotFound();
            }

            var descriptorType = await _context.DescriptorType.FindAsync(id);
            if (descriptorType == null)
            {
                return NotFound();
            }
            return View(descriptorType);
        }

        // POST: DescriptorTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DescriptorType descriptorType)
        {
            if (id != descriptorType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(descriptorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescriptorTypeExists(descriptorType.Id))
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
            return View(descriptorType);
        }

        // GET: DescriptorTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DescriptorType == null)
            {
                return NotFound();
            }

            var descriptorType = await _context.DescriptorType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descriptorType == null)
            {
                return NotFound();
            }

            return View(descriptorType);
        }

        // POST: DescriptorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DescriptorType == null)
            {
                return Problem("Entity set 'AnidoptContext.DescriptorType'  is null.");
            }
            var descriptorType = await _context.DescriptorType.FindAsync(id);
            if (descriptorType != null)
            {
                _context.DescriptorType.Remove(descriptorType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescriptorTypeExists(int id)
        {
          return (_context.DescriptorType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
