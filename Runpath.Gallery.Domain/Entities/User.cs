using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runpath.Gallery.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
