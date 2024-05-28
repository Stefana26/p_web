using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AuthorAddDTO
{
    public string Name { get; set; } = default!;
    public string? Nationality { get; set; } = default!;

    public string? DateOfBirth { get; set; } = default!;
    public string? Biography { get; set; }
   // public string? ImageUrl { get; set; }
   // public List<string> Books { get; set; } = new();
}
