using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public partial class ReturnPolicy
{
    public int PolicyId { get; set; }
    [Required]//yeu cau nhap bat buoc khong bo trong
    [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
    public string? Description { get; set; }
}
