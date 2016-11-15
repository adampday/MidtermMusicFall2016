using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MusicFall2016.Models
{
    public class MusicDbContext : IdentityDbContext<ApplicationUser>
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) 
            : base(options)
        {
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlaylistExtension>()
            .HasKey(t => new { t.playlistID, t.albumID });

            modelBuilder.Entity<PlaylistExtension>()
                .HasOne(pt => pt.playlist)
                .WithMany(p => p.list)
                .HasForeignKey(pt => pt.playlistID);

            modelBuilder.Entity<PlaylistExtension>()
                .HasOne(pt => pt.album)
                .WithMany(t => t.list)
                .HasForeignKey(pt => pt.albumID);
        }

        

    }
}
