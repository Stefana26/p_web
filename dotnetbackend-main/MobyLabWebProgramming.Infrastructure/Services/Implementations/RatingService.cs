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

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;
        public RatingService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse<RatingDTO>> GetRating(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAsync(new RatingProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

            return result != null ?
                ServiceResponse<RatingDTO>.ForSuccess(result) :
                ServiceResponse<RatingDTO>.FromError(CommonErrors.UserNotFound); // Pack the result or error into a ServiceResponse.
        }

        public async Task<ServiceResponse> AddRating(RatingAddDTO rating, UserDTO? requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
            {
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add books!", ErrorCodes.CannotAdd));
            }

            var result = await _repository.GetAsync(new RatingSpec(rating.Value), cancellationToken);

            var user = await _repository.GetAsync(new UserSpec(requestingUser.Email), cancellationToken);

            //if (result != null)
            //{
            //    return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The book already exists!", ErrorCodes.UserAlreadyExists));
            //}

            await _repository.AddAsync(new Rating
            {
                Value = rating.Value,
                BookId = rating.BookId,
                UserId = user.Id,
                User = user
            }, cancellationToken); // A new entity is created and persisted in the database.

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateRating(RatingUpdateDTO rating, UserDTO? requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the user!", ErrorCodes.CannotUpdate));
            }

            var entity = await _repository.GetAsync(new RatingSpec(rating.Id), cancellationToken);

            if (entity != null) // Verify if the book is not found, you cannot update an non-existing entity.
            {
                entity.Value = rating.Value ?? entity.Value;

                await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
            }

            return ServiceResponse.ForSuccess();
        }
        public async Task<ServiceResponse> DeleteRating(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
        {
            if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id) // Verify who can add the user, you can change this however you se fit.
            {
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the book!", ErrorCodes.CannotDelete));
            }

            await _repository.DeleteAsync<Rating>(id, cancellationToken); // Delete the entity.

            return ServiceResponse.ForSuccess();
        }
    }
}
