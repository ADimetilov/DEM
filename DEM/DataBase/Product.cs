using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEM;

public partial class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Desc { get; set; }

    public int? ManId { get; set; }

    public int? SupplierId { get; set; }

    public int? Cost { get; set; }

    [NotMapped]
    public Double NewCost { get; set; }

    public int? UnitId { get; set; }

    public int? Score { get; set; }

    public int? Sale { get; set; }

    public string? PathPhoto { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Manufacter? Man { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual Unit? Unit { get; set; }
}
