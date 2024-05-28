using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects
{
    public class FeedbackAddDTO
    {
        public string Q1 { get; set; } = default!;
        public string? Q2 { get; set; } = default!;
        public string? Q3 { get; set; } = default!;
        public string? Q4 { get; set; } = default!;
    }
}
