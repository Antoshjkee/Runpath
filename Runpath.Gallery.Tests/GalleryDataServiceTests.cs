using System.Threading.Tasks;
using Runpath.Gallery.Api.Infrastructure;
using Xunit;

namespace Runpath.Gallery.Tests
{
    public class GalleryDataServiceTests
    {
        [Fact]
        public async Task GalleryServiceEnsureAlbumsReturned()
        {
            var galleryService = new GalleryDataService();
            var albums = await galleryService.GetAlbumsAsync();

            Assert.NotEmpty(albums);
        }

        [Fact]
        public async Task GalleryServiceEnsurePhotosReturned()
        {
            var galleryService = new GalleryDataService();
            var photos = await galleryService.GetPhotosAsync();

            Assert.NotEmpty(photos);
        }
    }
}
