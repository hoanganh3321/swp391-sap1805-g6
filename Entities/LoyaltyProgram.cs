using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class LoyaltyProgram
{
    public int LoyaltyId { get; set; }

    public int? CustomerId { get; set; }

    public int? PointsAccumulated { get; set; }

    public int? PointsRedeemed { get; set; }

    public DateOnly? LastUpdated { get; set; }

    public virtual Customer? Customer { get; set; }
}
