using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public partial class Customer
{
    public int CustomerId { get; set; }
 
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public  required string LastName { get; set; } 
    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 6)]
    public required string Password { get; set; }
    [Phone(ErrorMessage = "Invalid phone number format.")]
    [StringLength(10)]
    public string? PhoneNumber { get; set; }
    [StringLength(255)]
    public string? Address { get; set; }

    public virtual ICollection<LoyaltyPoint> LoyaltyPoints { get; set; } = new List<LoyaltyPoint>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductReturn> ProductReturns { get; set; } = new List<ProductReturn>();
}
