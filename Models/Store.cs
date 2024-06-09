using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string? Location { get; set; }

    public decimal? Revenue { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
