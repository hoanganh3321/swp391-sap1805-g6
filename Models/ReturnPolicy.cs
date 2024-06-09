using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class ReturnPolicy
{
    public int PolicyId { get; set; }

    public string? Description { get; set; }
}
