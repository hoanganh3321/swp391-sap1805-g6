using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int? OrderId { get; set; }

    public DateOnly? IssueDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Order? Order { get; set; }
}
