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
        public string Description { get; set; } = default!;
        // Foreign key for the Book entity.
        public Guid? BookId { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

    }
}
