using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Infrastructure.Authorization;
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
            _feedbackService = feedback;
        }
    }
}
