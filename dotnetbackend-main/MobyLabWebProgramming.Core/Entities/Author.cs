using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Author : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Nationality { get; set; } = default!;
    public string? DateOfBirth { get; set; } = default!;
    public string? Biography { get; set; } = default!;

    // one-to-many relation between author and book
    public ICollection<Book>? Books { get; set; } = new List<Book>();
}
