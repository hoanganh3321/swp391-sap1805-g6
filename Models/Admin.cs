using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public partial class Admin
{
    public int AdminId { get; set; }
    [Required]
    [StringLength(100)]
    public string? AdminName { get; set; } 
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }
    [Required]
    [StringLength(255)]
    public string? Password { get; set; }
    [Required]
    public int? RoleId { get; set; }
    [StringLength(20)]
    public string? Phone { get; set; }
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual Role? Role { get; set; }
}
