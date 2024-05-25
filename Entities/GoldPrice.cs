using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class GoldPrice
{
    public int PriceId { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? GoldPrice1 { get; set; }
}
