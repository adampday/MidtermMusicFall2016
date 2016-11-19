using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using MusicFall2016.Models;
using Microsoft.AspNetCore.Authorization;

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
            ViewBag.User = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            return View();
     
        }

        [HttpPost]
        public IActionResult Playlists(Album album)
        {
            var user = User.Identity.Name;
            if (user == null)
            {
                return NotFound();
            }
            var playlist = _context.Playlists.SingleOrDefault(p => p.user.UserName == user);
            var list = new SelectList(_context.Albums, "AlbumID", "Title");
            ViewBag.AlbumList = list;
            return View("Playlists");  
        }

        [Authorize]
        public IActionResult Playlists()
        {
            var user = User.Identity.Name;
            if (user == null)
            {
                return NotFound();
            }
            var playlist = _context.Playlists.SingleOrDefault(p => p.user.UserName == user);
            if (playlist == null)
            {
                playlist = new Playlist();
                playlist.user.UserName = user;
                _context.Playlists.Add(playlist);
                _context.SaveChanges();
                playlist = _context.Playlists.SingleOrDefault(p => p.user.UserName == user);
            }
            var list = new SelectList(_context.Albums, "AlbumID", "Title");
            ViewBag.AlbumList = list;

                return View(playlist);
        }

        public IActionResult Create()
        {
            ViewBag.AlbumList = new SelectList(_context.Albums, "AlbumID", "Title");
            return View();
        }
    }
}
