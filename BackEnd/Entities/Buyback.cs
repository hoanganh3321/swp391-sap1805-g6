using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class Buyback
{
    public int BuybackId { get; set; }

    public int? ProductId { get; set; }

    public int? CustomerId { get; set; }

    public int? StaffId { get; set; }

    public DateOnly? BuybackDate { get; set; }

    public decimal? BuybackPrice { get; set; }

    public decimal? OriginalSalePrice { get; set; }

    public int? OriginalSaleId { get; set; }

    public virtual ICollection<BuybackProduct> BuybackProducts { get; set; } = new List<BuybackProduct>();

    public virtual Customer? Customer { get; set; }

    public virtual Order? OriginalSale { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Staff? Staff { get; set; }
}
