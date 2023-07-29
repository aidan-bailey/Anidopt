using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Anidopt.Controllers.SiteAdminControllers;

[Authorize(Roles = "SiteAdmin")]
public class DescriptorsController : Controller
{
    private readonly IDescriptorService _descriptorService;
    private readonly IDescriptorTypeService _descriptorTypeService;

    private string ViewPath(string name) => "~/Views/SiteAdmin/Descriptors/" + name + ".cshtml";

    public DescriptorsController(IDescriptorService descriptorService, IDescriptorTypeService descriptorTypeService)
    {
        _descriptorService = descriptorService;
        _descriptorTypeService = descriptorTypeService;
    }

    // GET: Descriptors
    public async Task<IActionResult> Index()
    {
        return View(ViewPath("Index"), await _descriptorService.GetAll().ToListAsync());
    }

    // GET: Descriptors/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || !_descriptorService.Initialised)
            return NotFound();
        var descriptor = await _descriptorService.GetByIdAsync((int)id);
        if (descriptor == null)
            return NotFound();
        return View(ViewPath("Details"), descriptor);
    }

    // GET: Descriptors/Create
    public async Task<IActionResult> Create()
    {
        ViewData["DescriptorTypeId"] = new SelectList(await _descriptorService.GetAll().ToListAsync(), "Id", "Name");
        return View(ViewPath("Create"));
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
            await _descriptorService.AddAsync(descriptor);
            return RedirectToAction(nameof(Index));
        }
        ViewData["DescriptorTypeId"] = new SelectList(await _descriptorTypeService.GetAll().ToListAsync(), "Id", "Name", descriptor.DescriptorTypeId);
        return View(ViewPath("Create"), descriptor);
    }

    // GET: Descriptors/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || !_descriptorService.Initialised)
            return NotFound();
        var descriptor = await _descriptorService.GetByIdAsync((int)id);
        if (descriptor == null)
            return NotFound();
        ViewData["DescriptorTypeId"] = new SelectList(await _descriptorTypeService.GetAll().ToListAsync(), "Id", "Name", descriptor.DescriptorTypeId);
        return View(ViewPath("Edit"), descriptor);
    }

    // POST: Descriptors/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,DescriptorTypeId,Name")] Descriptor descriptor)
    {
        if (id != descriptor.Id)
            return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                await _descriptorService.AddAsync(descriptor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_descriptorService.ExistsById(descriptor.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["DescriptorTypeId"] = new SelectList(await _descriptorTypeService.GetAll().ToListAsync(), "Id", "Name", descriptor.DescriptorTypeId);
        return View(ViewPath("Edit"), descriptor);
    }

    // GET: Descriptors/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || !_descriptorService.Initialised)
            return NotFound();
        var descriptor = await _descriptorService.GetByIdAsync((int)id);
        if (descriptor == null)
            return NotFound();
        return View(ViewPath("Delete"), descriptor);
    }

    // POST: Descriptors/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!_descriptorService.Initialised)
            return Problem("Entity set 'AnidoptContext.Descriptor'  is null.");
        await _descriptorService.EnsureDeletionByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
