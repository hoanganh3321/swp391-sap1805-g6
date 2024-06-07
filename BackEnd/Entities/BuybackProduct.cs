using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class BuybackProduct
{
    public int BuybackProductId { get; set; }

    public int? ProductId { get; set; }

    public int? BuybackId { get; set; }

    public virtual Buyback? Buyback { get; set; }

    public virtual Product? Product { get; set; }
}
