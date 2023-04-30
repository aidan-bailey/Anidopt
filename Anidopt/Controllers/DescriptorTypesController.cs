using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Controllers
{
    public class DescriptorTypesController : Controller
    {
        private readonly IDescriptorTypeService _descriptorTypeService;

        public DescriptorTypesController(IDescriptorTypeService descriptorTypeService)
        {
            _descriptorTypeService = descriptorTypeService;
        }

        // GET: DescriptorTypes
        public async Task<IActionResult> Index()
        {
              return _descriptorTypeService.Initialised ? 
                          View(await _descriptorTypeService.GetAllAsync()) :
                          Problem("Entity set 'AnidoptContext.DescriptorType'  is null.");
        }

        // GET: DescriptorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_descriptorTypeService.Initialised)
                return NotFound();
            var descriptorType = await _descriptorTypeService.GetByIdAsync((int)id);
            if (descriptorType == null)
                return NotFound();
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
                await _descriptorTypeService.AddAsync(descriptorType);
                return RedirectToAction(nameof(Index));
            }
            return View(descriptorType);
        }

        // GET: DescriptorTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_descriptorTypeService.Initialised)
                return NotFound();
            var descriptorType = await _descriptorTypeService.GetByIdAsync((int)id);
            if (descriptorType == null)
                return NotFound();
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
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await _descriptorTypeService.UpdateAsync(descriptorType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _descriptorTypeService.ExistsByIdAsync(descriptorType.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(descriptorType);
        }

        // GET: DescriptorTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_descriptorTypeService.Initialised)
                return NotFound();
            var descriptorType = await _descriptorTypeService.GetByIdAsync((int)id);
            if (descriptorType == null)
                return NotFound();
            return View(descriptorType);
        }

        // POST: DescriptorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_descriptorTypeService.Initialised)
                return Problem("Entity set 'AnidoptContext.DescriptorType'  is null.");
            await _descriptorTypeService.EnsureDeletionByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
