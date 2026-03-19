using System;
using System.Collections.Generic;

namespace DEM;

public partial class Order
{
    public int Id { get; set; }

    public int? StatusId { get; set; }
    public int? Art { get; set; }

    public string? Adres { get; set; }

    public DateOnly? DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }

    public virtual Status? Status { get; set; }
}
