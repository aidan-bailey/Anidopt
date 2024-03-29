﻿using Anidopt.Data;
using Anidopt.Identity;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Anidopt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers;

public class AccountsController : Controller {
    private readonly AnidoptContext _context;
    private readonly IAnidoptUserService _anidoptUserService;
    private readonly IOrganisationService _organisationService;
    private readonly UserManager<AnidoptUser> _userManager;
    private readonly SignInManager<AnidoptUser> _signInManager;
    private readonly RoleManager<AnidoptRole> _roleManager;

    public AccountsController(AnidoptContext context,
        IAnidoptUserService anidoptUserService,
        IOrganisationService organisationService,
        UserManager<AnidoptUser> userManager,
        SignInManager<AnidoptUser> signInManager,
        RoleManager<AnidoptRole> roleManager) {
        _context = context;
        _anidoptUserService = anidoptUserService;
        _organisationService = organisationService;
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
        if (user == null)
            return NotFound(); // TODO - Find a suitable response because something has clearly gone wrong.
        var users = user?.Organisation?.AnidoptUsers;
        return View(users);
    }

    public async Task<IActionResult> Details(int? id) {
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
            if (currentUser.Organisation.AnidoptUsers.Contains(user))
                return View(user);
        }

        return NotFound();
    }

    [Authorize(Roles = "SiteAdmin,OrganisationAdmin")]
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.AnidoptUser == null) {
            return NotFound();
        }

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
            return NotFound(); // TODO - Find a suitable response because something has clearly gone wrong.
        if (!currentUser.IsOrganisationAdmin)
            return NotFound();

        var user = await _context.AnidoptUser.FindAsync(id);
        if (user == null)
            return NotFound();

        if (user.OrganisationId != currentUser.OrganisationId)
            return NotFound();

        return View(user);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit([Bind("FirstName,LastName,Email,PhoneNumber")] AnidoptUser anidoptUser) {
        var user = await _userManager.GetUserAsync(User);
        user.FirstName = anidoptUser.FirstName;
        user.LastName = anidoptUser.LastName;
        user.Email = anidoptUser.Email;
        user.PhoneNumber = anidoptUser.PhoneNumber;
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

    [Authorize(Roles = "SiteAdmin,OrganisationAdmin")]
    public async Task<IActionResult> Delete(int id) {
        var currentUser = await _anidoptUserService.GetUserAsync(User);
        if (currentUser == null)
            return NotFound(); // TODO: Current user does not exist.

        var targetUser = await _anidoptUserService.GetByIdAsync(id);
        if (targetUser == null)
            return NotFound(); // TODO: Target user does not exist.

        if (!User.IsInRole("SiteAdmin"))
            if (!currentUser.Organisation.AnidoptUsers.Contains(targetUser))
                return NotFound(); // TODO: User does not have authority.

        if (currentUser.Id == targetUser.Id)
            return NotFound(); // TODO: Current user cannot delete their own account.

        await _anidoptUserService.EnsureDeletionByIdAsync(targetUser.Id);

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "SiteAdmin,OrganisationAdmin")]
    public async Task<IActionResult> Create() {
        if (User.IsInRole("SiteAdmin")) {
            var organisations = await _organisationService.GetAll().ToListAsync();
            ViewBag.Organisations = new SelectList(organisations, "Id", "Name");
        }
        else {
            var user = await _anidoptUserService.GetUserAsync(User);
            if (user == null)
                return NotFound();
            ViewBag.Organisations = new SelectList(new List<Organisation> { user.Organisation }, "Id", "Name");
        }
        return View("Register");
    }

    //public IActionResult Register() {
    //    if (_signInManager.IsSignedIn(User))
    //        return RedirectToAction("Index");
    //    else
    //        return View();
    //}

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model) {
        if (ModelState.IsValid) {
            var user = new AnidoptUser {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

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
