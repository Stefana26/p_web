﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record class AuthorUpdateDTO(Guid Id, string? Name, string? Nationality,string? DateOfBirth, string? Biography);
