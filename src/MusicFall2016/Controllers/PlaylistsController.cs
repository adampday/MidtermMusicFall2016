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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
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
                    .Include(p => p.name)
                    .Include(p => p.User)
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
