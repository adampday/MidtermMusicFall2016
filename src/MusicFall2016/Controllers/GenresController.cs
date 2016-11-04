using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class GenresController : Controller
    {
        private readonly MusicDbContext _context;

        public string ErrorMessage { get; private set; }

        public GenresController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Details()
        {
            var genres = _context.Genres.ToList();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                foreach(Genre test in _context.Genres)
                {
                    var name = test.Name;
                    if (name == genre.Name)
                    {
                        return RedirectToAction("Create");
                    }
                }
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Details");
            }
            return View();
        }



        public IActionResult Retrieve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = _context.Genres
                .SingleOrDefault(a => a.GenreID == id);

            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = _context.Genres
                .SingleOrDefault(a => a.GenreID == id);

            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [HttpPost]
        public IActionResult Update(Genre genre)
        {
            foreach (Genre test in _context.Genres)
            {
                var name = test.Name;
                if (name == genre.Name)
                {
                    return RedirectToAction("Update");
                }
            }
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }

        public IActionResult Genre(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                var album = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();

                var genre = _context.Genres.SingleOrDefault(a => a.GenreID == id);
                ViewData["Genre"] = genre.Name;     //sends data to the Genre view

                if (album == null)
                {
                    return NotFound();
                }
                return View(album);
            }

        }
    }
}