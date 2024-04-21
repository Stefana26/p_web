using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Rating : BaseEntity
{
    public int Value { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public Book Book { get; set; } = default!;
    public User User { get; set; } = default!;
}
