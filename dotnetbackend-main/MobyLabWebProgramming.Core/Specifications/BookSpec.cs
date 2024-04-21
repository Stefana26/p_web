using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class BookSpec : BaseSpec<BookSpec, Book>
{
    public BookSpec(Guid id) : base(id)
    {
    }

    public BookSpec(string title)
    {
        Query.Where(e => e.Title == title);
    }
}
