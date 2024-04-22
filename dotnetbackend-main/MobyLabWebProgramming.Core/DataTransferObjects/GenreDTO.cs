using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class GenreDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; } = default!;
    public ICollection<BookDTO>? Books { get; set; } = new List<BookDTO>();
}
