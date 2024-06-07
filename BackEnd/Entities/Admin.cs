using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class Admin
{
    public int AdminId { get; set; }

    public string AdminName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateOnly? HireDate { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual Role? Role { get; set; }
}
