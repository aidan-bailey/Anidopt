﻿using System;
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
    public class AnimalsController : Controller
    {
        private readonly AnidoptContext _context;

        public AnimalsController(AnidoptContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var anidoptContext = _context.Animal.Include(a => a.Breed).Include(a => a.Sex).Include(a => a.Size);
            return View(await anidoptContext.ToListAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.Breed)
                .Include(a => a.Sex)
                .Include(a => a.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "Name");
            ViewData["SexId"] = new SelectList(_context.Sex, "Id", "Name");
            ViewData["SizeId"] = new SelectList(_context.Size, "Id", "Name");
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,BirthDay,Description,BreedId,SexId,SizeId,Weight,Id")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "Name", animal.BreedId);
            ViewData["SexId"] = new SelectList(_context.Sex, "Id", "Name", animal.SexId);
            ViewData["SizeId"] = new SelectList(_context.Size, "Id", "Name", animal.SizeId);
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "Name", animal.BreedId);
            ViewData["SexId"] = new SelectList(_context.Sex, "Id", "Name", animal.SexId);
            ViewData["SizeId"] = new SelectList(_context.Size, "Id", "Name", animal.SizeId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,BirthDay,Description,BreedId,SexId,SizeId,Weight,Id")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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
            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "Name", animal.BreedId);
            ViewData["SexId"] = new SelectList(_context.Sex, "Id", "Name", animal.SexId);
            ViewData["SizeId"] = new SelectList(_context.Size, "Id", "Name", animal.SizeId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.Breed)
                .Include(a => a.Sex)
                .Include(a => a.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animal == null)
            {
                return Problem("Entity set 'AnidoptContext.Animal'  is null.");
            }
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                _context.Animal.Remove(animal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
          return (_context.Animal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
