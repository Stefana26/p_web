using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Rating : BaseEntity
{
    public int Value { get; set; }

    // many-to-one relation between rating and book
    public Guid BookId { get; set; }
    public Book Book { get; set; } = default!;

    // many-to-one relation between rating and user
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
