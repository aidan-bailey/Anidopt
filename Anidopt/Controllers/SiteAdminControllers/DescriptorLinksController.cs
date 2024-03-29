﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Anidopt.Controllers.SiteAdminControllers;

[Authorize(Roles = "SiteAdmin")]
public class DescriptorLinksController : Controller
{
    private readonly IDescriptorLinkService _descriptorLinkService;
    private readonly IAnimalService _animalService;
    private readonly IDescriptorService _descriptorService;

    private string ViewPath(string name) => "~/Views/SiteAdmin/DescriptorLinks/" + name + ".cshtml";

    public DescriptorLinksController(IDescriptorLinkService descriptorLinkService, IAnimalService animalService, IDescriptorService descriptorService)
    {
        _descriptorLinkService = descriptorLinkService;
        _animalService = animalService;
        _descriptorService = descriptorService;
    }

    // GET: DescriptorLinks
    public async Task<IActionResult> Index()
    {
        return View(ViewPath("Index"), await _descriptorLinkService.GetAll().ToListAsync());
    }

    // GET: DescriptorLinks/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || !_descriptorLinkService.Initialised)
            return NotFound();
        var DescriptorLink = await _descriptorLinkService.GetByIdAsync(id.Value);
        if (DescriptorLink == null)
            return NotFound();
        return View(ViewPath("Details"), DescriptorLink);
    }

    // GET: DescriptorLinks/Create
    public async Task<IActionResult> Create()
    {
        ViewData["AnimalId"] = new SelectList(await _animalService.GetAll().ToListAsync(), "Id", "Name");
        ViewData["DescriptorId"] = new SelectList(await _descriptorService.GetAll().ToListAsync(), "Id", "Name");
        return View(ViewPath("Create"));
    }

    // POST: DescriptorLinks/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,AnimalId,DescriptorId,DetailId")] DescriptorLink descriptorLink)
    {
        if (ModelState.IsValid)
        {
            await _descriptorLinkService.AddAsync(descriptorLink);
            return RedirectToAction(nameof(Index));
        }
        ViewData["AnimalId"] = new SelectList(await _animalService.GetAll().ToListAsync(), "Id", "Name", descriptorLink.AnimalId);
        ViewData["DescriptorId"] = new SelectList(await _descriptorService.GetAll().ToListAsync(), "Id", "Name", descriptorLink.DescriptorId);
        return View(ViewPath("Create"), descriptorLink);
    }

    // GET: DescriptorLinks/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || !_descriptorLinkService.Initialised)
            return NotFound();
        var descriptorLink = await _descriptorLinkService.GetByIdAsync(id.Value);
        if (descriptorLink == null)
            return NotFound();
        ViewData["AnimalId"] = new SelectList(await _animalService.GetAll().ToListAsync(), "Id", "Name", descriptorLink.AnimalId);
        ViewData["DescriptorId"] = new SelectList(await _descriptorService.GetAll().ToListAsync(), "Id", "Name", descriptorLink.DescriptorId);
        return View(ViewPath("Edit"), descriptorLink);
    }

    // POST: DescriptorLinks/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,AnimalId,DescriptorId,DetailId")] DescriptorLink descriptorLink)
    {
        if (id != descriptorLink.Id)
            return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                await _descriptorLinkService.UpdateAsync(descriptorLink);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_descriptorLinkService.ExistsById(descriptorLink.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["AnimalId"] = new SelectList(await _animalService.GetAll().ToListAsync(), "Id", "Name", descriptorLink.AnimalId);
        ViewData["DescriptorId"] = new SelectList(await _descriptorService.GetAll().ToListAsync(), "Id", "Name", descriptorLink.DescriptorId);
        return View(ViewPath("Edit"), descriptorLink);
    }

    // GET: DescriptorLinks/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || !_descriptorLinkService.Initialised)
            return NotFound();
        var descriptorLink = await _descriptorLinkService.GetByIdAsync(id.Value);
        if (descriptorLink == null)
            return NotFound();
        return View(ViewPath("Delete"), descriptorLink);
    }

    // POST: DescriptorLinks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!_descriptorLinkService.Initialised)
            return Problem("Entity set 'AnidoptContext.DescriptorLink'  is null.");
        await _descriptorLinkService.EnsureDeletionByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
