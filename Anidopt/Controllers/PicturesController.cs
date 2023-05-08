using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Controllers
{
    public class PicturesController : Controller
    {
        private readonly IPictureService _pictureService;

        public PicturesController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        // GET: Pictures
        public async Task<IActionResult> Index()
        {
            return View(await _pictureService.GetAllAsync());
        }

        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_pictureService.Initialised)
            {
                return NotFound();
            }

            var picture = await _pictureService.GetByIdAsync(id.Value);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // GET: Pictures/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AnimalId"] = new SelectList(await _pictureService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Image,AnimalId,Showcase,Id")] Picture picture)
        {
            if (ModelState.IsValid)
            {
                await _pictureService.AddAsync(picture);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(await _pictureService.GetAllAsync(), "Id", "Name", picture.AnimalId);
            return View(picture);
        }

        // GET: Pictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_pictureService.Initialised)
            {
                return NotFound();
            }

            var picture = await _pictureService.GetByIdAsync(id.Value);
            if (picture == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(await _pictureService.GetAllAsync(), "Id", "Name", picture.AnimalId);
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Image,AnimalId,Showcase,Id")] Picture picture)
        {
            if (id != picture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pictureService.UpdateAsync(picture);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_pictureService.ExistsById(picture.Id))
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
            ViewData["AnimalId"] = new SelectList(await _pictureService.GetAllAsync(), "Id", "Name", picture.AnimalId);
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_pictureService.Initialised)
            {
                return NotFound();
            }

            var picture = await _pictureService.GetByIdAsync(id.Value);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_pictureService.Initialised)
            {
                return Problem("Entity set 'AnidoptContext.Picture'  is null.");
            }
            await _pictureService.EnsureDeletionByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
