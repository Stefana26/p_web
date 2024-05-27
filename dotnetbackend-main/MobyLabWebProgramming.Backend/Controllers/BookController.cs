using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
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
    public class BookController : AuthorizedController
    {
        private readonly IBookService _bookService;

        public BookController(IUserService userService, IBookService bookService) : base(userService)
        {
            _bookService = bookService;
        }
        [Authorize]
        [HttpGet("{id:guid}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
        public async Task<ActionResult<RequestResponse<BookDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _bookService.GetBook(id)) :
                this.ErrorMessageResult<BookDTO>(currentUser.Error);
        }
        [Authorize]
        [HttpGet] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetPage.
        public async Task<ActionResult<RequestResponse<PagedResponse<BookDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination) // The FromQuery attribute will bind the parameters matching the names of
                                                                                                                                             // the PaginationSearchQueryParams properties to the object in the method parameter.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _bookService.GetBooks(pagination)) :
                this.ErrorMessageResult<PagedResponse<BookDTO>>(currentUser.Error);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RequestResponse>> Add([FromBody] BookAddDTO body)
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _bookService.AddBook(body, currentUser.Result)) :
                this.ErrorMessageResult(currentUser.Error);
        }

        [HttpPut] // This attribute will make the controller respond to a HTTP PUT request on the route /api/User/Update.
        public async Task<ActionResult<RequestResponse>> Update([FromBody] BookUpdateDTO book) // The FromBody attribute indicates that the parameter is deserialized from the JSON body.
        {
            //var currentUser = await GetCurrentUser();

            //return currentUser.Result != null ?
            //    this.FromServiceResponse(await _bookService.UpdateBook(book, currentUser.Result)) :
            //    this.ErrorMessageResult(currentUser.Error);
            var result = this.FromServiceResponse(await _bookService.UpdateBook(book, null));
            return result;
        }

        [Authorize]
        [HttpDelete("{id:guid}")] // This attribute will make the controller respond to a HTTP DELETE request on the route /api/User/Delete/<some_guid>.
        public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
        {
            var currentUser = await GetCurrentUser();

            return currentUser.Result != null ?
                this.FromServiceResponse(await _bookService.DeleteBook(id)) :
                this.ErrorMessageResult(currentUser.Error);
        }
    }
}
