using Anidopt.Models;
using Anidopt.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Anidopt.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AnidoptUser> _userManager;
    private readonly SignInManager<AnidoptUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<AnidoptUser> userManager,
                                  SignInManager<AnidoptUser> signInManager,
                                  RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        if (_signInManager.IsSignedIn(User))
            return View(await _userManager.GetUserAsync(User));
        else
            return RedirectToAction("Login");
    }

    #region Register

    public IActionResult Register()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Account");
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
            return RedirectToAction("Account");
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
