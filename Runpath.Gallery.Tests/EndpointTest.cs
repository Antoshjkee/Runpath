using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Runpath.Gallery.Api;
using Xunit;

namespace Runpath.Gallery.Tests
{

    public class EndpointTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public EndpointTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/users")]
        [InlineData("/api/users/1")]
        [InlineData("/api/users/1/albums")]
        public async Task EnsureUsersEndpointExists(string path)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/api/users/0")]
        [InlineData("/api/users/0/albums/0")]
        public async Task NotFoundReturnedForNonExistingUser(string path)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(path);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
