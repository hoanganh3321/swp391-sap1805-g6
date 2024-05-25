using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateOnly? HireDate { get; set; }

    public int? QuayId { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Buyback> Buybacks { get; set; } = new List<Buyback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Quay? Quay { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<SalesPoint> SalesPoints { get; set; } = new List<SalesPoint>();
}
