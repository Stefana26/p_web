using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects
{
    public class ReviewAddDTO
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public int Rating { get; set; }
    }
}
