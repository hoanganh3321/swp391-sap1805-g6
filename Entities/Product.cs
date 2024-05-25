using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace swp391_sap1805_g6.Entities;

public partial class Product
{
    public int ProductId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Type { get; set; }
    [Required]
    public string? Barcode { get; set; }
    [Required]
    public decimal? Weight { get; set; }
    [Required]
    public decimal? Price { get; set; }
    [Required]
    public decimal? ManufacturingCost { get; set; }
    [Required]
    public decimal? StoneCost { get; set; }
    [Required]
    public string? WarrantyInfo { get; set; }
    //public int? Duration { get; set; }
    public bool? IsBuyback { get; set; }

    public virtual ICollection<BuybackProduct> BuybackProducts { get; set; } = new List<BuybackProduct>();

    public virtual ICollection<Buyback> Buybacks { get; set; } = new List<Buyback>();

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<Quay> Quays { get; set; } = new List<Quay>();
}
