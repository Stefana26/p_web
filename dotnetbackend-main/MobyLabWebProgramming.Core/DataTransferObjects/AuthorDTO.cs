﻿using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AuthorDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Nationality { get; set; } = default!;
    public string? Biography { get; set; }
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<BookDTO>? Books { get; set; } = new List<BookDTO>();
    //  public string? ImageUrl { get; set; }
    //  public List<string> Books { get; set; } = new();
}
