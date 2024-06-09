using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class GoldPriceDisplay
{
    public int DisplayId { get; set; }

    public int DeviceId { get; set; }

    public string? Location { get; set; }

    public decimal GoldPrice { get; set; }

    public DateTime LastUpdated { get; set; }
}
