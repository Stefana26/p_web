using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IReviewService
{
    Task<ServiceResponse<ReviewDTO>> GetReview(Guid id, CancellationToken cancellationToken = default);
   // Task<ServiceResponse> AddReview(ReviewAddDTO review, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    //Task<ServiceResponse> UpdateReview(ReviewUpdateDTO review, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    //Task<ServiceResponse> DeleteReview(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
