using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Review : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public int Rating { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; } = default!;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
