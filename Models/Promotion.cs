using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }
    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateOnly? StartDate { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateOnly? EndDate { get; set; }
    [Required]
    [Range(0, 99.99)]
    public decimal? Discount { get; set; }
    [Required]
    public bool? Approved { get; set; }
    public int? ApprovedBy { get; set; }
    [Required]
    public int? Points { get; set; }

    public virtual Admin? ApprovedByNavigation { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
