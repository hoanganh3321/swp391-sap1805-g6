using System;
using System.Collections.Generic;

namespace swp391_sap1805_g6.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
