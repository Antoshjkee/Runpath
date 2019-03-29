using Microsoft.EntityFrameworkCore;
using Runpath.Gallery.Domain.Entities;

namespace Runpath.Gallery.Domain
{
    public class GalleryContext: DbContext
    {
        public GalleryContext(DbContextOptions options) : base(options) {}

        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(x => x.Id);

            var albumBuilder = modelBuilder.Entity<Album>();
            albumBuilder.HasKey(x => x.Id);
            albumBuilder.HasMany(x => x.Photos);

            var photoBuilder = modelBuilder.Entity<Photo>();
            photoBuilder.HasKey(x => x.Id);
            photoBuilder.HasOne(x => x.Album);
        }
    }
}
