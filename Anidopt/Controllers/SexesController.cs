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
    public class SexesController : Controller
    {
        private readonly AnidoptContext _context;
        private readonly ISexService _sexService;

        public SexesController(AnidoptContext context, ISexService sexService)
        {
            _context = context;
            _sexService = sexService;
        }

        // GET: Sexes
        public async Task<IActionResult> Index() => _sexService.Initialised
            ? View(await _sexService.GetAllAsync()) 
            : Problem("Entity set 'AnidoptContext.Sex'  is null.");

        // GET: Sexes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_sexService.Initialised) return NotFound();
            var sex = await _sexService.GetByIdAsync((int)id);
            if (sex == null) return NotFound();
            return View(sex);
        }

        // GET: Sexes/Create
        public IActionResult Create() => View();

        // POST: Sexes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Sex sex)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sex);
        }

        // GET: Sexes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_sexService.Initialised) return NotFound();
            var sex = await _sexService.GetByIdAsync((int)id);
            if (sex == null) return NotFound();
            return View(sex);
        }

        // POST: Sexes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Sex sex)
        {
            if (id != sex.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _sexService.ExistsByIdAsync(sex.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sex);
        }

        // GET: Sexes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_sexService.Initialised) return NotFound();
            var sex = await _sexService.GetByIdAsync((int)id);
            if (sex == null) return NotFound();
            return View(sex);
        }

        // POST: Sexes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_sexService.Initialised) return Problem("Entity set 'AnidoptContext.Sex'  is null.");
            await _sexService.EnsureDeletionByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
