using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;

        // one-to-many relation between genre and book
        public ICollection<Book>? Books { get; set; } = new List<Book>();

    }
}
