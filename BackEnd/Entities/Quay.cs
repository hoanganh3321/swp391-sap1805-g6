using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class Quay
{
    public int QuayId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public decimal? Revenue { get; set; }

    public virtual ICollection<SalesPoint> SalesPoints { get; set; } = new List<SalesPoint>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
