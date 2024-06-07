using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class Warranty
{
    public int WarrantyId { get; set; }

    public int? ProductId { get; set; }

    public int? Duration { get; set; }

    public virtual Product? Product { get; set; }
}
