using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications;

public class GenreProjectionSpec : BaseSpec<GenreProjectionSpec, Genre, GenreDTO>
{
    protected override Expression<Func<Genre, GenreDTO>> Spec => e => new GenreDTO
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description
    };

    public GenreProjectionSpec(Guid id) : base(id)
    {
    }
}
