using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects
{
    public class RatingDTO
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public int Value { get; set; }
    }
}
