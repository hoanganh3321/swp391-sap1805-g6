using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public partial class Store
{
    public int StoreId { get; set; }
    [StringLength(100)]
    public string StoreName { get; set; } = null!;
    [StringLength(255)]
    public string? Location { get; set; }
    [Range(0, double.MaxValue)]
    public decimal? Revenue { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
    
}
//từ id của staff sẽ truy được ra store mà nó làm việc task<Store> từ đó này sẽ lấy được revenue của store gán revenue = giá đã qua promotion  