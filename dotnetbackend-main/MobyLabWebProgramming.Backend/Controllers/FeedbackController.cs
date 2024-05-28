using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FeedbackController : AuthorizedController
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IUserService userService, IFeedbackService feedbackService) : base(userService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<ActionResult<RequestResponse>> Add([FromBody] FeedbackAddDTO body)
        {
            var result = this.FromServiceResponse(await _feedbackService.AddFeedback(body));

            return result;
        }
    }
}
