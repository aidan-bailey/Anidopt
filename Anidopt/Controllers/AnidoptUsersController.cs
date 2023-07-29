using Anidopt.Data;
using Anidopt.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers;

[Authorize(Roles = "SiteAdmin")]
public class AnidoptUsersController : Controller {
    private readonly AnidoptContext _context;

    public AnidoptUsersController(AnidoptContext context) {
        _context = context;
    }

    // GET: Users
    public async Task<IActionResult> Index() {
        return _context.AnidoptUser != null ?
                    View(await _context.AnidoptUser.ToListAsync()) :
                    Problem("Entity set 'AnidoptContext.User'  is null.");
    }

    // GET: Users/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null || _context.AnidoptUser == null)             return NotFound();

        var user = await _context.AnidoptUser
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)             return NotFound();

        return View(user);
    }

    // GET: Users/Create
    public IActionResult Create() {
        return View();
    }

    // POST: Users/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FirstName,LastName,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AnidoptUser user) {
        if (ModelState.IsValid) {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: Users/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.AnidoptUser == null)             return NotFound();

        var user = await _context.AnidoptUser.FindAsync(id);
        if (user == null)             return NotFound();
        return View(user);
    }

    // POST: Users/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AnidoptUser user) {
        if (id != user.Id)             return NotFound();

        if (ModelState.IsValid) {
            try {
                _context.Update(user);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!UserExists(user.Id))                     return NotFound();
else                     throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: Users/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null || _context.AnidoptUser == null)             return NotFound();

        var user = await _context.AnidoptUser
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)             return NotFound();

        return View(user);
    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id) {
        if (_context.AnidoptUser == null)             return Problem("Entity set 'AnidoptContext.User'  is null.");
        var user = await _context.AnidoptUser.FindAsync(id);
        if (user != null)             _context.AnidoptUser.Remove(user);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UserExists(int id) {
        return (_context.AnidoptUser?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
