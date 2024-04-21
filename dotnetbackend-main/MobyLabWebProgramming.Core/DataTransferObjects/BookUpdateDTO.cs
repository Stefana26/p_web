namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record class BookUpdateDTO(Guid Id, string? Title, string? Author, string? Description, int? Pages);
