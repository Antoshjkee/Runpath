using System.Collections.Generic;

namespace Runpath.Gallery.Api.Models
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<PhotoDto> Photos { get; set; }
    }
}
