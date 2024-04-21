using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
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
    public BookService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<BookDTO>> GetBook(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new BookProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ?
            ServiceResponse<BookDTO>.ForSuccess(result) :
            ServiceResponse<BookDTO>.FromError(CommonErrors.UserNotFound); // Pack the result or error into a ServiceResponse.
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
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The book already exists!", ErrorCodes.UserAlreadyExists));
        }

        await _repository.AddAsync(new Book
        {
            Title = book.Title,
            Description = book.Description,
            Pages = book.Pages
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateBook(BookUpdateDTO book, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the user!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new BookSpec(book.Id), cancellationToken);

        if (entity != null) // Verify if the book is not found, you cannot update an non-existing entity.
        {
            entity.Title = book.Title ?? entity.Title;
            entity.Description = book.Description ?? entity.Description;
            entity.Pages = book.Pages ?? entity.Pages;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteBook(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the book!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<User>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }
}
