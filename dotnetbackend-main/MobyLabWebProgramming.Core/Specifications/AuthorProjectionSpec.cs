using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
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
            Genre = new GenreAddDTO
            {
                Name = book.Genre.Name
                // Add other properties of GenreDTO if needed
            },
            GenreId = book.GenreId,
            Description = book.Description,
            Pages = book.Pages
        }).ToList()

    };

    public AuthorProjectionSpec(Guid id) : base(id)
    {
    }

    public AuthorProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
                                                                   // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
