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

public class BookProjectionSpec : BaseSpec<BookProjectionSpec, Book, BookDTO>
{
    protected override Expression<Func<Book, BookDTO>> Spec => e => new BookDTO
    {
        Id = e.Id,
        Title = e.Title,
        Author = new AuthorAddDTO
        {
            Name = e.Author.Name,
            Nationality = e.Author.Nationality,
            Biography = e.Author.Biography
        },
        AuthorId = e.AuthorId,
        Genre =  new GenreAddDTO
        {
            Name = e.Genre.Name,
            Description = e.Genre.Description
        },
        GenreId = e.GenreId,
        Description = e.Description,
        Pages = e.Pages,
        Ratings = e.Ratings.Select(rating => new RatingEssential
        {
            Value = rating.Value,
            UserId = rating.UserId
        }).ToList(),
        Reviews = e.Reviews.Select(review => new ReviewEssential
        {
            UserId = review.UserId,
            Comment = review.Content,
        }).ToList()
    };

    public BookProjectionSpec(Guid id) : base(id)
    {
    }

    public BookProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Title, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
                                                                  // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
