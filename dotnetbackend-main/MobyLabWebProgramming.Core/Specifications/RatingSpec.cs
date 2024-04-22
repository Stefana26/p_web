using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

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
