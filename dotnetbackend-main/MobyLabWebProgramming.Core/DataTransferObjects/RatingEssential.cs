﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class RatingEssential
{
    public int Value { get; set; }
    public Guid? UserId { get; set; }
}
