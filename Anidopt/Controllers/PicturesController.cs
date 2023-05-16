﻿using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Linq;

namespace Anidopt.Controllers
{
    public class PicturesController : Controller
    {
        private readonly IPictureService _pictureService;
        private readonly IAnimalService _animalService;

        public PicturesController(IPictureService pictureService, IAnimalService animalService)
        {
            _pictureService = pictureService;
            _animalService = animalService;
        }

        // GET: Pictures
        public async Task<IActionResult> Index()
        {
            var pictures = await _pictureService.GetAllAsync();
            var imagesBase64 = pictures.ToDictionary(
                p => p.Id, 
                p => "data:image/png;base64," + Convert.ToBase64String(p.Image)
            );
            foreach (var picture in pictures)
                picture.Image = null;
            ViewBag.ImagesBase64 = imagesBase64;
            return View(pictures);
        }

        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_pictureService.Initialised) return NotFound();
            var picture = await _pictureService.GetByIdAsync(id.Value); 
            if (picture == null) return NotFound();
            ViewBag.ImageBase64 = Convert.ToBase64String(picture.Image);
            picture.Image = null;
            return View(picture);
        }

        // GET: Pictures/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AnimalId"] = new SelectList(await _animalService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Showcase,Id,FormFile")] PictureUpload pictureUpload)
        {
            if (ModelState.IsValid)
            {
                if (!PictureUpload.SupportedImageTypes.Contains(pictureUpload.FormFile.ContentType))
                {
                    ModelState.AddModelError("FormFile", "File is bad type.");
                }
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await pictureUpload.FormFile.CopyToAsync(memoryStream);
                        var picture = new Picture
                        {
                            Image = memoryStream.ToArray(),
                            AnimalId = pictureUpload.AnimalId,
                            Showcase = pictureUpload.Showcase
                        };
                        await _pictureService.AddAsync(picture);
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["AnimalId"] = new SelectList(await _animalService.GetAllAsync(), "Id", "Name", pictureUpload.AnimalId);
            return View(pictureUpload);
        }

        // GET: Pictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_pictureService.Initialised)
                return NotFound();

            var picture = await _pictureService.GetByIdAsync(id.Value);
            if (picture == null)
                return NotFound();
            ViewData["AnimalId"] = new SelectList(await _animalService.GetAllAsync(), "Id", "Name", picture.AnimalId);
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
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _pictureService.UpdateAsync(picture);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_pictureService.ExistsById(picture.Id))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(await _animalService.GetAllAsync(), "Id", "Name", picture.AnimalId);
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_pictureService.Initialised)
                return NotFound();
            var picture = await _pictureService.GetByIdAsync(id.Value);
            if (picture == null)
                return NotFound();
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
