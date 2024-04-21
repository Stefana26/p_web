using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IRatingService
{
    Task<ServiceResponse<RatingDTO>> GetRating(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResponse> AddRating(RatingAddDTO rating, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    Task<ServiceResponse> UpdateRating(RatingUpdateDTO rating, UserDTO? requestingUser, CancellationToken cancellationToken = default);
   // Task<ServiceResponse> DeleteRating(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
