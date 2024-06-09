using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public partial class LoyaltyPoint
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Points must be greater than 0")]
    public int Points { get; set; }
    [Required]
    public DateTime LastUpdated { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
