using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Runpath.Gallery.Domain.Entities;

namespace Runpath.Gallery.Api.Infrastructure
{
    public class GalleryDataService
    {
        private readonly string _endpoint = "http://jsonplaceholder.typicode.com";

        public async Task<IEnumerable<Album>> GetAlbumsAsync() => await GetGalleryCollection<Album>("albums");
        public async Task<IEnumerable<Photo>> GetPhotosAsync() => await GetGalleryCollection<Photo>("photos");

        private async Task<IEnumerable<T>> GetGalleryCollection<T>(string segmentName) where T : class
        {
            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.GetAsync($"{_endpoint}/{segmentName}");

                if (!responseMessage.IsSuccessStatusCode) return default(IEnumerable<T>);

                var contentString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(contentString);
            }
        }
    }
}
