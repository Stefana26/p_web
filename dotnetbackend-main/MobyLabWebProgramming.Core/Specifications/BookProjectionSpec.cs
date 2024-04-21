using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications;

public class BookProjectionSpec : BaseSpec<BookProjectionSpec, Book, BookDTO>
{
    protected override Expression<Func<Book, BookDTO>> Spec => e => new BookDTO
    {
        Id = e.Id,
        Title = e.Title,
        Description = e.Description,
        Pages = e.Pages
    };

    public BookProjectionSpec(Guid id) : base(id)
    {
    }
}
