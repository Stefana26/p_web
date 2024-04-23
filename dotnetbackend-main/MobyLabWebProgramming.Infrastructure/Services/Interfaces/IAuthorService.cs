using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IAuthorService
{
    Task<ServiceResponse<AuthorDTO>> GetAuthor(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResponse<PagedResponse<AuthorDTO>>> GetAuthors(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    Task<ServiceResponse> AddAuthor(AuthorAddDTO author, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    Task<ServiceResponse> UpdateAuthor(AuthorUpdateDTO author, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    Task<ServiceResponse> DeleteAuthor(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
