using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications;

public class RatingSpec : BaseSpec<RatingSpec, Rating>
{
    public RatingSpec(Guid id) : base(id)
    {
    }

    public RatingSpec(int value)
    {
        Query.Where(e => e.Value == value);
    }
}
