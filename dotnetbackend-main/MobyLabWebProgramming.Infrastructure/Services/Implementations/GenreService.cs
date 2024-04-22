using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    public GenreService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse<GenreDTO>> GetGenre(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new GenreProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ?
            ServiceResponse<GenreDTO>.ForSuccess(result) :
            ServiceResponse<GenreDTO>.FromError(CommonErrors.UserNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse> AddGenre(GenreAddDTO genre, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add books!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new GenreSpec(genre.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The book already exists!", ErrorCodes.UserAlreadyExists));
        }

        await _repository.AddAsync(new Genre
        {
            Name = genre.Name,
            Description = genre.Description ?? null
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateGenre(GenreUpdateDTO genre, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the user!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new GenreSpec(genre.Id), cancellationToken);

        if (entity != null) // Verify if the book is not found, you cannot update an non-existing entity.
        {
            entity.Name = genre.Name ?? entity.Name;
            entity.Description = genre.Description ?? entity.Description;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteGenre(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the genre!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Genre>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }
}
