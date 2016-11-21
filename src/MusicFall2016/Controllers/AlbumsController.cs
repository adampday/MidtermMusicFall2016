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

        public async Task<IActionResult> Details(String searchString, String sortOrder)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_asc" : "title_desc";
            ViewData["ArtistSortParm"] = String.IsNullOrEmpty(sortOrder) ? "artist_asc" : "artist_desc";
            ViewData["GenreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "genre_asc" : "genre_desc";
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_asc" : "price_desc";
            ViewData["LikeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "like_asc" : "like_desc";
            var album = from a in _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                        select a;
            switch (sortOrder)
            {
                case "title_asc":
                    album = album.OrderBy(a => a.Title);
                    break;
                case "title_desc":
                    album = album.OrderByDescending(a => a.Title);                   
                    break;
                case "artist_asc":
                    album = album.OrderBy(a => a.Artist.Name);
                    break;
                case "artist_desc":
                    album = album.OrderByDescending(a => a.Artist.Name);                  
                    break;
                case "genre_asc":
                    album = album.OrderBy(a => a.Genre.Name);
                    break;
                case "genre_desc":
                    album = album.OrderByDescending(a => a.Genre.Name);
                    break;
                case "price_asc":
                    album = album.OrderBy(a => a.Price);
                    break;
                case "price_desc":
                    album = album.OrderByDescending(a => a.Price);                   
                    break;
                case "like_asc":
                    album = album.OrderBy(a => a.Like);
                    break;
                case "like_desc":
                    album = album.OrderByDescending(a => a.Like);                    
                    break;
                
            }

            //if (sortOrder == "")
            //{
            //    ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_asc" : "title_desc";
            //    ViewData["ArtistSortParm"] = String.IsNullOrEmpty(sortOrder) ? "artist_asc" : "artist_desc";
            //    ViewData["GenreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "genre_asc" : "genre_desc";
            //    ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_asc" : "price_desc";
            //    ViewData["LikeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "like_asc" : "like_desc";
            //}

            if (searchString != null)
            {
                var albumsSearch = from m in _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                                   select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    albumsSearch = albumsSearch.Where(s => s.Title.Contains(searchString) || s.Artist.Name.Contains(searchString) || s.Genre.Name.Contains(searchString) || s.Price.ToString().Contains(searchString)); //return album
                    //albumsSearch = albumsSearch.Where(s => s.Artist.Name.Contains(searchString)); //return artist
                    //albumsSearch = albumsSearch.Where(s => s.Genre.Name.Contains(searchString)); //return genre
                }

                return View(albumsSearch.ToList());
            }
            else
            {

                var albums = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();


                
            }
            return View(await album.AsNoTracking().ToListAsync());
        }

        //new change


        public IActionResult Create()
        {
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Album album, String addNewArtist, String addNewGenre)
        {
            if (ModelState.IsValid)
            {
                if (addNewArtist != null)
                {
                    foreach(var artist in _context.Artists.ToList())
                    {
                        String name = artist.Name;
                        if (name == addNewArtist)
                        {
                            addNewArtist = "";
                        }
                    }
                    if (addNewArtist != "")
                    {
                        Artist artist = new Artist();
                        artist.Name = addNewArtist;
                        artist.Bio = "";
                        _context.Artists.Add(artist);
                        _context.SaveChanges();
                        album.Artist = _context.Artists.Last();
                    }
                    
                }

                if (addNewGenre != null)
                {
                    foreach (var genres in _context.Genres.ToList())
                    {
                        String name = genres.Name;
                        if (name == addNewGenre)
                        {
                            addNewGenre = "";
                        }

                    }
                    if (addNewGenre != "")
                    {
                        Genre genre = new Genre();
                        genre.Name = addNewGenre;
                        _context.Genres.Add(genre);
                        _context.SaveChanges();
                        album.Genre = _context.Genres.Last();
                    }

                }
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
            var album = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a => a.AlbumID == id);

            if (id == null)
            {
                return NotFound();
            }
            

            if (album == null)
            {
                return NotFound();
            }

            var recommended = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre).ToList();
            String albumString = "";
            var count = 0;

            foreach(var albums in recommended)
            {
                if (albums.Like >= 5 && count < 5 && albums.AlbumID != album.AlbumID)
                {
                    count += 1;
                    albumString += albums.Title + " ";
                }
            }

            ViewData["readAlbum"] = albumString;


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
            album.Like += 1;
            _context.SaveChanges();
            return RedirectToAction("Details");

        }
    }

}
