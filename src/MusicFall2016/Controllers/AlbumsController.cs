using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicDbContext _context;

        public AlbumsController(MusicDbContext context)
        {
            _context = context;
        }

        public IActionResult Details(String searchString)
        {
            if (searchString != null)
            {
                var albumsSearch = from m in _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                                   select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    albumsSearch = albumsSearch.Where(s => s.Title.Contains(searchString)); //return album
                    //albumsSearch = albumsSearch.Where(s => s.Artist.Name.Contains(searchString)); //return artist
                    //albumsSearch = albumsSearch.Where(s => s.Genre.Name.Contains(searchString)); //return genre
                }

                return View(albumsSearch.ToList());
            }
            else { 

            var albums = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();
            

            return View(albums);
            }

        }




        public IActionResult Create()
        {
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Albums.Add(album);
                _context.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }

        public IActionResult Retrieve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a => a.AlbumID == id);

            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        public IActionResult Update(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
                ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
                var album = _context.Albums
                    .Include(a => a.Artist)
                    .Include(a => a.Genre)
                    .SingleOrDefault(a => a.AlbumID == id);

                if (album == null)
                {
                    return NotFound();
                }
                return View(album);
            }
        }
        [HttpPost]
        public IActionResult Update(Album album)
        {
            _context.Albums.Update(album);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
        public IActionResult Delete(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                var album = _context.Albums
                    .Include(a => a.Artist)
                    .Include(a => a.Genre)
                    .SingleOrDefault(a => a.AlbumID == id);

                if (album == null)
                {
                    return NotFound();
                }
                return View(album);
            }
        }
        [HttpPost]
        public IActionResult Delete(Album album)
        {
            _context.Albums.Remove(album);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
        /*
                public IActionResult Details(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }
                    var album = _context.Albums
                        .Include(a => a.Artist)
                        .Include(a => a.Artist)
                        .SingleOrDefault(a => a.AlbumID == id);

                    if (album == null)
                    {
                        return NotFound();
                    }
                    return View(album);
                }
        */
        public IActionResult Like(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = _context.Albums
                        .Include(a => a.Artist)
                        .Include(a => a.Genre)
                        .SingleOrDefault(a => a.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            album.Like = album.Like + 1;
            _context.SaveChanges();
            return RedirectToAction("Details");

        }
    }


}
