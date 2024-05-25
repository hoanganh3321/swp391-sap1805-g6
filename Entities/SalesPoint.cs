using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class SalesPoint
{
    public int SalesPointId { get; set; }

    public int? QuayId { get; set; }

    public int? StaffId { get; set; }

    public decimal? TotalSales { get; set; }

    public DateOnly? SalesDate { get; set; }

    public virtual Quay? Quay { get; set; }

    public virtual Staff? Staff { get; set; }
}
