using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Anidopt.Controllers.SiteAdminControllers;

[Authorize(Roles = "SiteAdmin")]
public class OrganisationsController : Controller
{
    private readonly IOrganisationService _organisationService;

    private string ViewPath(string name) => "~/Views/SiteAdmin/DescriptorLinks/" + name + ".cshtml";

    public OrganisationsController(IOrganisationService organisationService)
    {
        _organisationService = organisationService;
    }

    // GET: Organisations
    public async Task<IActionResult> Index()
    {
        if (!_organisationService.Initialised) return Problem("Entity set 'AnidoptContext.Organisation'  is null.");
        var organisations = await _organisationService.GetAll().ToListAsync();
        return View(ViewPath("Index"), organisations);
    }

    // GET: Organisations/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || !_organisationService.Initialised) return NotFound();
        var organisation = await _organisationService.GetByIdAsync((int)id);
        if (organisation == null) return NotFound();
        return View(ViewPath("Details"), organisation);
    }

    // GET: Organisations/Create
    public IActionResult Create()
    {
        return View(ViewPath("Create"));
    }

    // POST: Organisations/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Animals")] Organisation organisation)
    {
        if (ModelState.IsValid)
        {
            await _organisationService.AddAsync(organisation);
            return RedirectToAction(nameof(Index));
        }
        return View(ViewPath("Create"), organisation);
    }

    // GET: Organisations/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || !_organisationService.Initialised) return NotFound();
        var organisation = await _organisationService.GetByIdAsync((int)id);
        if (organisation == null) return NotFound();
        return View(ViewPath("Edit"), organisation);
    }

    // POST: Organisations/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Organisation organisation)
    {
        if (id != organisation.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await _organisationService.UpdateAsync(organisation);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_organisationService.ExistsById(organisation.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(ViewPath("Edit"), organisation);
    }

    // GET: Organisations/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || !_organisationService.Initialised) return NotFound();
        var organisation = await _organisationService.GetByIdAsync((int)id);
        if (organisation == null) return NotFound();
        return View(ViewPath("Delete"), organisation);
    }

    // POST: Organisations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!_organisationService.Initialised) return Problem("Entity set 'AnidoptContext.Organisation'  is null.");
        await _organisationService.EnsureDeletionByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
