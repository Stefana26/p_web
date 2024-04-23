using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorController : AuthorizedController
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IUserService userService, IAuthorService authorService) : base(userService)
        {
            _authorService = authorService;
        }
        [Authorize]
        [HttpGet("{id:guid}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
        public async Task<ActionResult<RequestResponse<AuthorDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _authorService.GetAuthor(id)) :
                this.ErrorMessageResult<AuthorDTO>(currentUser.Error);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RequestResponse>> Add([FromBody] AuthorAddDTO body)
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _authorService.AddAuthor(body, currentUser.Result)) :
                this.ErrorMessageResult(currentUser.Error);
        }
        [Authorize]
        [HttpGet] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetPage.
        public async Task<ActionResult<RequestResponse<PagedResponse<AuthorDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination) // The FromQuery attribute will bind the parameters matching the names of
                                                                                                                                             // the PaginationSearchQueryParams properties to the object in the method parameter.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _authorService.GetAuthors(pagination)) :
                this.ErrorMessageResult<PagedResponse<AuthorDTO>>(currentUser.Error);
        }

        [Authorize]
        [HttpPut] // This attribute will make the controller respond to a HTTP PUT request on the route /api/User/Update.
        public async Task<ActionResult<RequestResponse>> Update([FromBody] AuthorUpdateDTO author) // The FromBody attribute indicates that the parameter is deserialized from the JSON body.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _authorService.UpdateAuthor(author, currentUser.Result)) :
                this.ErrorMessageResult(currentUser.Error);
        }

        [Authorize]
        [HttpDelete("{id:guid}")] // This attribute will make the controller respond to a HTTP DELETE request on the route /api/User/Delete/<some_guid>.
        public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _authorService.DeleteAuthor(id)) :
                this.ErrorMessageResult(currentUser.Error);
        }
    }
}
