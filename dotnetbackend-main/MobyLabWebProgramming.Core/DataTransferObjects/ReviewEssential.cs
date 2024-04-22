using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ReviewEssential
{
    public Guid UserId { get; set; }
    public string Comment { get; set; }
}
