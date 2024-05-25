using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string? Name { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? DiscountRate { get; set; }

    public bool? IsApproved { get; set; }

    public string? ApprovedBy { get; set; }

    public virtual Admin? ApprovedByNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
