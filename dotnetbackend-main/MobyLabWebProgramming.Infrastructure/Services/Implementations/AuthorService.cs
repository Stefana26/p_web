﻿using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
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

public class AuthorService : IAuthorService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    public AuthorService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<AuthorDTO>> GetAuthor(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new AuthorProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ?
            ServiceResponse<AuthorDTO>.ForSuccess(result) :
            ServiceResponse<AuthorDTO>.FromError(CommonErrors.AuthorNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<AuthorDTO>>> GetAuthors(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new AuthorProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<AuthorDTO>>.ForSuccess(result);// Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse> AddAuthor(AuthorAddDTO author, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add authors!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new AuthorSpec(author.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The author already exists!", ErrorCodes.AuthorAlreadyExists));
        }

        await _repository.AddAsync(new Author
        {
            Name = author.Name,
            Nationality = author.Nationality ?? null,
            DateOfBirth = author.DateOfBirth ?? null,
            Biography = author.Biography ?? null,
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateAuthor(AuthorUpdateDTO author, CancellationToken cancellationToken = default)
    {

        var entity = await _repository.GetAsync(new AuthorSpec(author.Id), cancellationToken);

        if (entity != null) // Verify if the book is not found, you cannot update an non-existing entity.
        {
            entity.Name = author.Name ?? entity.Name;
            entity.Biography = author.Biography ?? entity.Biography;
            entity.DateOfBirth = author.DateOfBirth ?? entity.DateOfBirth;
            entity.Nationality = author.Nationality ?? entity.Nationality;
            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        } else
        {
            return ServiceResponse<AuthorDTO>.FromError(CommonErrors.AuthorNotFound);
        }

        return ServiceResponse.ForSuccess();
    }
    public async Task<ServiceResponse> DeleteAuthor(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the author!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Author>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }
}
