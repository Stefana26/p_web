using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IBookService
{
    Task<ServiceResponse<BookDTO>> GetBook(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResponse<PagedResponse<BookDTO>>> GetBooks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    Task<ServiceResponse> AddBook(BookAddDTO book, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    Task<ServiceResponse> UpdateBook(BookUpdateDTO book, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    Task<ServiceResponse> DeleteBook(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
