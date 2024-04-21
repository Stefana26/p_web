using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Pages { get; set; }

        // many-to-one relation between book and author
        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }= default!;

        // many-to-one relation between book and genre
        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; }

        // one-to-many relation between book and review
        public ICollection<Review>? Reviews { get; set; }
        // one-to-many relation between book and rating
        public ICollection<Rating>? Ratings { get; set; }
    }
}
