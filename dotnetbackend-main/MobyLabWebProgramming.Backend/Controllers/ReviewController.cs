using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    public class ReviewController : AuthorizedController
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IUserService userService, IReviewService reviewService) : base(userService)
        {
            _reviewService = reviewService;
        }
    }
}
