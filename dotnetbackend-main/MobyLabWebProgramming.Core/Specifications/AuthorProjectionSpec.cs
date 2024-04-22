using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications;

public class AuthorProjectionSpec : BaseSpec<AuthorProjectionSpec, Author, AuthorDTO>
{
    protected override Expression<Func<Author, AuthorDTO>> Spec => e => new AuthorDTO
    {
        Id = e.Id,
        Name = e.Name,
        Biography = e.Biography,
        Nationality = e.Nationality,
        Books = e.Books.Select(book => new BookDTO
        {
            Id = book.Id,
            Title = book.Title,
            Genre = new GenreDTO
            {
                Id = book.Genre.Id,
                Name = book.Genre.Name
                // Add other properties of GenreDTO if needed
            },
            Description = book.Description,
            Pages = book.Pages
        }).ToList()

    };

    public AuthorProjectionSpec(Guid id) : base(id)
    {
    }
}
