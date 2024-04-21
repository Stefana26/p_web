using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications;

public class GenreSpec : BaseSpec<GenreSpec, Genre>
{
    public GenreSpec(Guid id) : base(id)
    {
    }

    public GenreSpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}
