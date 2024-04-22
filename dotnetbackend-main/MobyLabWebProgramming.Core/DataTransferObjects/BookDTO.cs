using MobyLabWebProgramming.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class BookDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AuthorDTO Author { get; set; } = default!;
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? AuthorId { get; set; } = default!;
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GenreDTO Genre { get; set; } = default!;
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? GenreId { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Pages { get; set; }
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<RatingEssential>? Ratings { get; set; } = new List<RatingEssential>();
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<ReviewEssential>? Reviews { get; set; } = new List<ReviewEssential>();
}
