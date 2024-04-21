using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class BookDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Pages { get; set; }
}
