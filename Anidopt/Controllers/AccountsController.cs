using Anidopt.Data;
using Anidopt.Models;
using Anidopt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Anidopt.Controllers;

public class AccountsController : Controller
{
    private readonly AnidoptContext _context;
    private readonly UserManager<AnidoptUser> _userManager;
    private readonly SignInManager<AnidoptUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountsController(AnidoptContext context, UserManager<AnidoptUser> userManager,
                                  SignInManager<AnidoptUser> signInManager,
                                  RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        return View(await _userManager.GetUserAsync(User));
    }

    [Authorize]
    public async Task<IActionResult> Edit()
    {
        return View(await _userManager.GetUserAsync(User));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit([Bind("FirstName,LastName")] AnidoptUser anidoptUser)
    {
        var user = await _userManager.GetUserAsync(User);
        user.FirstName = anidoptUser.FirstName;
        user.LastName = anidoptUser.LastName;
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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
        return View(anidoptUser);
    }
    private bool UserExists(string id)
    {
        return (_context.AnidoptUser?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    #region Register

    public IActionResult Register()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Index");
        else
            return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AnidoptUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

        }
        return View(model);
    }

    #endregion

    #region Login

    [HttpGet]
    public IActionResult Login()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Index");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel user)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

        }
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        if (_signInManager.IsSignedIn(User))
            await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    #endregion
}
