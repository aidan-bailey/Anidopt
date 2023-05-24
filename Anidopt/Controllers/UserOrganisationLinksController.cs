using Anidopt.Data;
using Anidopt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers;

[Authorize(Roles = "SiteAdmin")]
public class UserOrganisationLinksController : Controller
{
    private readonly AnidoptContext _context;

    public UserOrganisationLinksController(AnidoptContext context)
    {
        _context = context;
    }

    // GET: UserOrganisationLinks
    public async Task<IActionResult> Index()
    {
        var anidoptContext = _context.UserOrganisationLink.Include(u => u.Organisation).Include(u => u.User);
        return View(await anidoptContext.ToListAsync());
    }

    // GET: UserOrganisationLinks/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.UserOrganisationLink == null)
        {
            return NotFound();
        }

        var userOrganisationLink = await _context.UserOrganisationLink
            .Include(u => u.Organisation)
            .Include(u => u.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (userOrganisationLink == null)
        {
            return NotFound();
        }

        return View(userOrganisationLink);
    }

    // GET: UserOrganisationLinks/Create
    public IActionResult Create()
    {
        ViewData["OrganisationId"] = new SelectList(_context.Organisation, "Id", "Name");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: UserOrganisationLinks/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserId,OrganisationId,IsAdmin,Id")] UserOrganisationLink userOrganisationLink)
    {
        if (ModelState.IsValid)
        {
            _context.Add(userOrganisationLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["OrganisationId"] = new SelectList(_context.Organisation, "Id", "Name", userOrganisationLink.OrganisationId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userOrganisationLink.UserId);
        return View(userOrganisationLink);
    }

    // GET: UserOrganisationLinks/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.UserOrganisationLink == null)
        {
            return NotFound();
        }

        var userOrganisationLink = await _context.UserOrganisationLink.FindAsync(id);
        if (userOrganisationLink == null)
        {
            return NotFound();
        }
        ViewData["OrganisationId"] = new SelectList(_context.Organisation, "Id", "Name", userOrganisationLink.OrganisationId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userOrganisationLink.UserId);
        return View(userOrganisationLink);
    }

    // POST: UserOrganisationLinks/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("UserId,OrganisationId,IsAdmin,Id")] UserOrganisationLink userOrganisationLink)
    {
        if (id != userOrganisationLink.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(userOrganisationLink);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserOrganisationLinkExists(userOrganisationLink.Id))
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
        ViewData["OrganisationId"] = new SelectList(_context.Organisation, "Id", "Name", userOrganisationLink.OrganisationId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userOrganisationLink.UserId);
        return View(userOrganisationLink);
    }

    // GET: UserOrganisationLinks/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.UserOrganisationLink == null)
        {
            return NotFound();
        }

        var userOrganisationLink = await _context.UserOrganisationLink
            .Include(u => u.Organisation)
            .Include(u => u.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (userOrganisationLink == null)
        {
            return NotFound();
        }

        return View(userOrganisationLink);
    }

    // POST: UserOrganisationLinks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.UserOrganisationLink == null)
        {
            return Problem("Entity set 'AnidoptContext.UserOrganisationLink'  is null.");
        }
        var userOrganisationLink = await _context.UserOrganisationLink.FindAsync(id);
        if (userOrganisationLink != null)
        {
            _context.UserOrganisationLink.Remove(userOrganisationLink);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UserOrganisationLinkExists(int id)
    {
      return (_context.UserOrganisationLink?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
