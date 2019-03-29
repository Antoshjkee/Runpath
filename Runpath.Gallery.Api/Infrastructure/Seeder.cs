using System.Linq;
using Runpath.Gallery.Domain;
using Runpath.Gallery.Domain.Entities;

namespace Runpath.Gallery.Api.Infrastructure
{
    public class Seeder
    {
        private readonly GalleryContext _context;
        private readonly GalleryDataService _galleryDataService;

        public Seeder(GalleryContext context, GalleryDataService galleryDataService)
        {
            _context = context;
            _galleryDataService = galleryDataService;
        }

        public void EnsureCreated()
        {
            _context.Database.EnsureCreated();
            var albums = _galleryDataService.GetAlbumsAsync().Result;
            _context.Set<Album>().AddRange(albums);

            _context.Set<User>().AddRange(albums
                .Select(x => x.UserId)
                .Distinct()
                .Select(x => new User
                {
                    Id = x
                }));

            var photos = _galleryDataService.GetPhotosAsync().Result;
            _context.Set<Photo>().AddRange(photos);
            _context.SaveChangesAsync();
        }
    }
}
