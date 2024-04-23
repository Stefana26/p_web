using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class ReviewController : AuthorizedController
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IUserService userService, IReviewService reviewService) : base(userService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("{id:guid}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
        public async Task<ActionResult<RequestResponse<ReviewDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _reviewService.GetReview(id)) :
                this.ErrorMessageResult<ReviewDTO>(currentUser.Error);
        }

        [HttpPost]
        public async Task<ActionResult<RequestResponse>> Add([FromBody] ReviewAddDTO body)
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _reviewService.AddReview(body, currentUser.Result)) :
                this.ErrorMessageResult(currentUser.Error);
        }

        [HttpPut] // This attribute will make the controller respond to a HTTP PUT request on the route /api/User/Update.
        public async Task<ActionResult<RequestResponse>> Update([FromBody] ReviewUpdateDTO review) // The FromBody attribute indicates that the parameter is deserialized from the JSON body.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _reviewService.UpdateReview(review, currentUser.Result)) :
                this.ErrorMessageResult(currentUser.Error);
        }

        [HttpDelete("{id:guid}")] // This attribute will make the controller respond to a HTTP DELETE request on the route /api/User/Delete/<some_guid>.
        public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _reviewService.DeleteReview(id)) :
                this.ErrorMessageResult(currentUser.Error);
        }
    }
}
