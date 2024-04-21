using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications
{
    public class ReviewProjectionSpec : BaseSpec<ReviewProjectionSpec, Review, ReviewDTO>
    {
        protected override Expression<Func<Review, ReviewDTO>> Spec => e => new ReviewDTO
        {
            Title = e.Title,
            Content = e.Content,
            BookId = e.BookId,
            UserId = e.UserId
        };

        public ReviewProjectionSpec(Guid id) : base(id)
        {
        }
    }
}
