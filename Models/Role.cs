using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
