using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string? Name { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? Discount { get; set; }

    public bool? Approved { get; set; }

    public string? ApprovedBy { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
