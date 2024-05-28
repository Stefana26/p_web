using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobyLabWebProgramming.Core.Specifications
{
    public class FeedbackSpec : BaseSpec<FeedbackSpec, Feedback>
    {
        public FeedbackSpec(Guid id) : base(id)
        {
        }

        public FeedbackSpec(string q1)
        {
            Query.Where(e => e.Q1 == q1);
        }
    }
}
