using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class Dashboard
{
    public int DashboardId { get; set; }

    public string? Metrics { get; set; }

    public string? Data { get; set; }

    public byte[] GeneratedAt { get; set; } = null!;
}
