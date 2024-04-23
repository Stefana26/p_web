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

public class GenreProjectionSpec : BaseSpec<GenreProjectionSpec, Genre, GenreDTO>
{
    protected override Expression<Func<Genre, GenreDTO>> Spec => e => new GenreDTO
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
         Books = e.Books.Select(book => new BookDTO
         {
             Id = book.Id,
             Title = book.Title,
             Author = new AuthorAddDTO
             {
                 Name = book.Author.Name
             },
             AuthorId = book.AuthorId,
             Description = book.Description,
             Pages = book.Pages
         }).ToList()
    };

    public GenreProjectionSpec(Guid id) : base(id)
    {
    }

    public GenreProjectionSpec(string? search)
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
