using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Runpath.Gallery.Api;
using Runpath.Gallery.Domain.Entities;
using Xunit;

namespace Runpath.Gallery.Tests
{
    public class UsersControllerTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public UsersControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("api/users")]
        public async Task EnsureUsersExist(string path)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(path);

            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(await response.Content.ReadAsStringAsync());
            Assert.NotEmpty(users);
        }

        [Theory]
        [InlineData("api/users/1")]
        public async Task EnsureSingleUserExists(string path)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(path);

            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());

            Assert.Equal(1, user.Id);
        }
        
    }
}
