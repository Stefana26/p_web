using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications;

public class AuthorSpec : BaseSpec<AuthorSpec, Author>
{
    public AuthorSpec(Guid id) : base(id)
    {
    }

    public AuthorSpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}
