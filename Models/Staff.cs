using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string StaffName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? RoleId { get; set; }

    public int? StoreId { get; set; }

    public string? Phone { get; set; }

    public DateOnly? HireDate { get; set; }

    public virtual Role? Role { get; set; }

    public virtual Store? Store { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
