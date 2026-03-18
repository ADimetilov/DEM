using System;
using System.Collections.Generic;

namespace DEM;

public partial class Manufacter
{
    public int Id { get; set; }

    public string? Manufacter1 { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
