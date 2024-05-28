using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Implementation;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class BookService : IBookService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IAuthorService _authorService;
    private readonly IGenreService _genreService;
    public BookService(IRepository<WebAppDatabaseContext> repository, 
        IAuthorService authorService, 
        IGenreService genreService)
    {
        _repository = repository;
        _authorService = authorService;
        _genreService = genreService;
    }

    public async Task<ServiceResponse<BookDTO>> GetBook(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new BookProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ?
            ServiceResponse<BookDTO>.ForSuccess(result) :
            ServiceResponse<BookDTO>.FromError(CommonErrors.BookNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<BookDTO>>> GetBooks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new BookProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<BookDTO>>.ForSuccess(result);// Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse> AddBook(BookAddDTO book, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add books!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new BookSpec(book.Title), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The book already exists!", ErrorCodes.BookAlreadyExists));
        }


        // check the status of author
        var author = await _repository.GetAsync(new AuthorSpec(book.Author), cancellationToken);

        if (author == null)
        {
            var authorName = new AuthorAddDTO
            {
                Name = book.Author
            };
            var authorResult = await _authorService.AddAuthor(authorName);

            author = await _repository.GetAsync(new AuthorSpec(book.Author), cancellationToken);
        }

        // check the status of the genre
        var genre = await _repository.GetAsync(new GenreSpec(book.Genre), cancellationToken);

        if (genre == null)
        {
            var genreName = new GenreAddDTO
            {
                Name = book.Genre
            };
            var genreResult = await _genreService.AddGenre(genreName, requestingUser);
            genre = await _repository.GetAsync(new GenreSpec(book.Genre), cancellationToken);
        }

        await _repository.AddAsync(new Book
        {
            Title = book.Title,
            ISBN = book.ISBN,
            Description = book.Description,
            Pages = book.Pages,
            Author = author,
            Genre = genre,
            GenreId = genre.Id,
            AuthorId = author.Id
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateBook(BookUpdateDTO book, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        //if (requestingUser != null && (requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel))
        //{
        //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the user!", ErrorCodes.CannotUpdate));
        //}

        var entity = await _repository.GetAsync(new BookSpec(book.Title), cancellationToken);

        if (entity != null) // Verify if the book is not found, you cannot update an non-existing entity.
        {
            entity.Title = book.Title ?? entity.Title;
            entity.Description = book.Description ?? entity.Description;
            entity.AuthorId = entity.AuthorId;
            // check the status of author
            var author = await _repository.GetAsync(new AuthorSpec(book.Author), cancellationToken);

            if (author == null)
            {
                var authorName = new AuthorAddDTO
                {
                    Name = book.Author
                };
                var authorResult = await _authorService.AddAuthor(authorName);

                author = await _repository.GetAsync(new AuthorSpec(book.Author), cancellationToken);
                entity.AuthorId = author.Id;
            }

            var genre = await _repository.GetAsync(new GenreSpec(book.Genre), cancellationToken);

            if (genre == null)
            {
                var genreName = new GenreAddDTO
                {
                    Name = book.Genre
                };
                var genreResult = await _genreService.AddGenre(genreName, requestingUser);
                genre = await _repository.GetAsync(new GenreSpec(book.Genre), cancellationToken);
                entity.GenreId = genre.Id;
            }
            entity.Author = author ?? entity.Author;
            
            entity.Genre = genre ?? entity.Genre;
            entity.GenreId = entity.GenreId;


            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        } else
        {
            return ServiceResponse<BookDTO>.FromError(CommonErrors.BookNotFound);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteBook(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the book!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Book>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }
}
