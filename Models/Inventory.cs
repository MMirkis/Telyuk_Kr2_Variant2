using System;
using System.Collections.Generic;

namespace Variant2.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public string Article { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public string? Description { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int Status { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
