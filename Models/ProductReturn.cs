using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class ProductReturn
{
    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public DateTime ReturnDate { get; set; }

    public string? ReturnReason { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
