using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications;

public class AuthorProjectionSpec : BaseSpec<AuthorProjectionSpec, Author, AuthorDTO>
{
    protected override Expression<Func<Author, AuthorDTO>> Spec => e => new AuthorDTO
    {
        Id = e.Id,
        Name = e.Name,
        Surname = e.Surname,
        Biography = e.Biography
    };

    public AuthorProjectionSpec(Guid id) : base(id)
    {
    }
}
