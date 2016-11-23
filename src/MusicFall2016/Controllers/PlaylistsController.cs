using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using MusicFall2016.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using static MusicFall2016.Models.MusicDbContext;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MusicDbContext _context;


        public PlaylistsController(MusicDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            ViewBag.User = user;
            var playlists = _context.Playlists.Where(p => p.User == user).ToList();

            return View(playlists);

        }
        public IActionResult Add(int? id)
        {
           
            var playlists = _context.Playlists.Where(p => p.User.UserName == User.Identity.Name).ToList();
            ViewBag.Playlist = new SelectList(playlists, "playListID", "name");
            Album album = _context.Albums.Where(a => a.AlbumID == id).Single();
   
            return View(album);
        }

        [HttpPost]
        public IActionResult Add(int AlbumID, int PlayListID)
        {
           // var playlist = _context.Playlists.SingleOrDefault(p => p.playListID == PlayListID);
            PlaylistExtension playListAlbums = new PlaylistExtension { albumID = AlbumID, playlistID = PlayListID };

                _context.Add(playListAlbums);
                _context.SaveChanges();
                return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                playlist.User = user;
                _context.Add(playlist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = _context.Playlists
                .SingleOrDefault(p => p.playListID == id);

            if (playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }

        [HttpPost]
        public IActionResult Update(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }

                var playlist = _context.Playlists
                    
                    .SingleOrDefault(p => p.playListID == id);

                if (playlist == null)
                {
                    return NotFound();
                }
                return View(playlist);
            }
        }
        [HttpPost]
        public IActionResult Delete(Playlist playlist)
        {
            _context.Playlists.Remove(playlist);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
