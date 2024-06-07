using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace swp391_sap1805_g6.Entities;

public partial class Customer
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Phone { get; set; }
    [Required]
    public string? Email { get; set; }

    public int? LoyaltyPoints { get; set; }
    [Required]
    public string? Address { get; set; }

    public virtual ICollection<Buyback> Buybacks { get; set; } = new List<Buyback>();

    public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; } = new List<LoyaltyProgram>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
