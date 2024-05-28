using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<ServiceResponse> AddFeedback(FeedbackAddDTO feedback, CancellationToken cancellationToken = default);
    }
}
