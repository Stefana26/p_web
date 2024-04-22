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
    public class ReviewService : IReviewService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public ReviewService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<ReviewDTO>> GetReview(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetAsync(new ReviewProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

            return result != null ?
                ServiceResponse<ReviewDTO>.ForSuccess(result) :
                ServiceResponse<ReviewDTO>.FromError(CommonErrors.UserNotFound); // Pack the result or error into a ServiceResponse.
        }

        public async Task<ServiceResponse> AddReview(ReviewAddDTO review, UserDTO? requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
            {
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add reviews!", ErrorCodes.CannotAdd));
            }

            var result = await _repository.GetAsync(new ReviewSpec(review.Title), cancellationToken);

            if (result != null)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The review already exists!", ErrorCodes.UserAlreadyExists));
            }

            var user = await _repository.GetAsync(new UserSpec(requestingUser.Email), cancellationToken);

            await _repository.AddAsync(new Review
            {
                Title = review.Title,
                Content = review.Content,
                BookId = review.BookId,
                User = user
            }, cancellationToken); // A new entity is created and persisted in the database.

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateReview(ReviewUpdateDTO review, UserDTO? requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the review!", ErrorCodes.CannotUpdate));
            }

            var entity = await _repository.GetAsync(new ReviewSpec(review.Id), cancellationToken);

            if (entity != null) // Verify if the book is not found, you cannot update an non-existing entity.
            {
                entity.Title = review.Title ?? entity.Title;
                entity.Content = review.Content ?? entity.Content;

                await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
            }

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteReview(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
        {
            if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id) // Verify who can add the user, you can change this however you se fit.
            {
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the review!", ErrorCodes.CannotDelete));
            }

            await _repository.DeleteAsync<Review>(id, cancellationToken); // Delete the entity.

            return ServiceResponse.ForSuccess();
        }
    }
}
