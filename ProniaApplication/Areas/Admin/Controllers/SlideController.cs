﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApplication.DAL;
using ProniaApplication.Models;

namespace ProniaApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        private readonly AppDBContext _context;

        public SlideController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync();
            return View(slides);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Slide slide)
        {
            if (!slide.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "File type is not correct");
                return View();
            }
            if (slide.Photo.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Photo", "File size must be less than 2 MB");
                return View();
            }

            return Content(slide.Photo.FileName);

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            //await _context.Slides.AddAsync(slide);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }
    }
}
