

using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IGenreService
{
    Task<ServiceResponse<GenreDTO>> GetGenre(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResponse> AddGenre(GenreAddDTO genre, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    Task<ServiceResponse> UpdateGenre(GenreUpdateDTO genre, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    Task<ServiceResponse> DeleteGenre(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
