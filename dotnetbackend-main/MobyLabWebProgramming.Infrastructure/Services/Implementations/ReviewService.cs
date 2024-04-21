using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
