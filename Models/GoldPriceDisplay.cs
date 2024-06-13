using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public partial class GoldPriceDisplay
{
    public int DisplayId { get; set; }
    [Required]
    public int DeviceId { get; set; }
    [MaxLength(100)]
    public string? Location { get; set; }
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Gold Price must be a positive value.")]
    public decimal GoldPrice { get; set; }
    [Required]
    public DateTime LastUpdated { get; set; }
}
