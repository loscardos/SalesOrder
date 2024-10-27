using System;
using System.Collections.Generic;

namespace SalesOrder.Shared;

public partial class SoItem
{
    public long SoItemId { get; set; }
    public long SoOrderId { get; set; }
    public SoOrder? SoOrder { get; set; }
    public string ItemName { get; set; } = null!;
    public int Quantity { get; set; }
    public double Price { get; set; }
}
