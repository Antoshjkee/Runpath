using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Runpath.Gallery.Api;
using Runpath.Gallery.Domain.Entities;
using Xunit;

namespace Runpath.Gallery.Tests
{
    public class AlbumsControllerTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AlbumsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("api/users/1/albums")]
        public async Task EnsureAlbumsExist(string path)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(path);

            var albums = JsonConvert.DeserializeObject<IEnumerable<Album>>(await response.Content.ReadAsStringAsync());
            Assert.NotEmpty(albums);
        }

        [Theory]
        [InlineData("api/users/1/albums/1")]
        public async Task EnsureAlbumAndPhotoesExist(string path)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(path);

            var album = JsonConvert.DeserializeObject<Album>(await response.Content.ReadAsStringAsync());

            Assert.Equal(1, album.Id);
            Assert.NotEmpty(album.Photos);
        }
    }
}
