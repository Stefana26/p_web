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
             Author = new AuthorDTO
             {
                 Id = book.Author.Id,
                 Name = book.Author.Name
             },
             Description = book.Description,
             Pages = book.Pages
         }).ToList()
    };

    public GenreProjectionSpec(Guid id) : base(id)
    {
    }
}
