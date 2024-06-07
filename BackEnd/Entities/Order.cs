using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace swp391_sap1805_g6.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? StaffId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? Discount { get; set; }
    [JsonIgnore]
    public virtual ICollection<Buyback> Buybacks { get; set; } = new List<Buyback>();
    [JsonIgnore]
    public virtual Customer? Customer { get; set; }
    [JsonIgnore]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    [JsonIgnore]
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    [JsonIgnore]
    public virtual Staff? Staff { get; set; }
    [JsonIgnore]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
