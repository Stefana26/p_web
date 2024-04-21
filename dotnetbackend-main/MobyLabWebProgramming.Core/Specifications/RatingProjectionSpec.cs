using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications;

public class RatingProjectionSpec : BaseSpec<RatingProjectionSpec, Rating, RatingDTO>
{
    protected override Expression<Func<Rating, RatingDTO>> Spec => e => new RatingDTO
    {
        Id = e.Id,
        Value = e.Value,
        BookId = e.BookId,
       // UserId = e.UserId
    };

    public RatingProjectionSpec(Guid id) : base(id)
    {
    }
}
