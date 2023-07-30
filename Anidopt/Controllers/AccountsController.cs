using Anidopt.Data;
using Anidopt.Identity;
using Anidopt.Services.Interfaces;
using Anidopt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers;

public class AccountsController : Controller {
    private readonly AnidoptContext _context;
    private readonly IAnidoptUserService _anidoptUserService;
    private readonly UserManager<AnidoptUser> _userManager;
    private readonly SignInManager<AnidoptUser> _signInManager;
    private readonly RoleManager<AnidoptRole> _roleManager;

    public AccountsController(AnidoptContext context, 
        IAnidoptUserService anidoptUserService,
        UserManager<AnidoptUser> userManager,
        SignInManager<AnidoptUser> signInManager,
        RoleManager<AnidoptRole> roleManager) {
        _context = context;
        _anidoptUserService = anidoptUserService;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [Authorize(Roles = "SiteAdmin,OrganisationAdmin")]
    public async Task<IActionResult> Index() {
        if (User.IsInRole("SiteAdmin")) {
            return View(await _anidoptUserService.GetAll().ToListAsync());
        }
        var user = await _anidoptUserService.GetUserAsync(User);
        if (user == null) {
            return NotFound(); // TODO - Find a suitable response because something has clearly gone wrong.
        }
        var users = await _anidoptUserService.GetAdministratedUsersAsync(user.Id);
        return View(users);
    }

    public async Task<IActionResult> Details(int ?id) {
        if (!_anidoptUserService.Initialised) 
            return NotFound();
        if (id == null) {
            if (!_signInManager.IsSignedIn(User))
                return NotFound();
            var currentUser = await _anidoptUserService.GetUserAsync(User);
            if (currentUser == null)
                return NotFound();
            return View(currentUser);
        }

        var user = await _anidoptUserService.GetByIdAsync(id.Value);
        if (user == null)
            return NotFound();

        if (User.IsInRole("SiteAdmin"))
            return View(user);

        if (User.IsInRole("OrganisationAdmin")) {
            var currentUser = await _anidoptUserService.GetUserAsync(User);
            if (currentUser == null)
                return NotFound();
        }

        return NotFound();
    }

    [Authorize(Roles = "SiteAdmin,OrganisationAdmin")]
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.AnidoptUser == null) {
            return NotFound();
        }

        var user = await _context.AnidoptUser.FindAsync(id);
        if (user == null) {
            return NotFound();
        }

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) {
            return NotFound(); // TODO - Find a suitable response because something has clearly gone wrong.
        }
        if (user.Id == currentUser.Id) {
            return View(user);
        }

        if (!await _anidoptUserService.HasEditRights(User, id.Value))
            return NotFound();

        return View(user);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit([Bind("FirstName,LastName")] AnidoptUser anidoptUser) {
        var user = await _userManager.GetUserAsync(User);
        user.FirstName = anidoptUser.FirstName;
        user.LastName = anidoptUser.LastName;
        if (ModelState.IsValid) {
            try {
                _context.Update(user);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!UserExists(user.Id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(anidoptUser);
    }
    private bool UserExists(int id) {
        return (_context.AnidoptUser?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    #region Register

    public IActionResult Register() {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Index");
        else
            return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model) {
        if (ModelState.IsValid) {
            var user = new AnidoptUser {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("index", "Home");
            }

            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error.Description);
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

        }
        return View(model);
    }

    #endregion

    #region Login

    [HttpGet]
    public IActionResult Login() {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Index");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel user) {
        if (ModelState.IsValid) {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

            if (result.Succeeded) {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

        }
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Logout() {
        if (_signInManager.IsSignedIn(User))
            await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    #endregion
}
