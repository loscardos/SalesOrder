using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SalesOrder.Shared;

public partial class SoOrder
{
    public long SoOrderId { get; set; }
    public string OrderNo { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public int ComCustomerId { get; set; }
    public ComCustomer? ComCustomer { get; set; }
    public Collection<SoItem> SoItems { get; set; } = new Collection<SoItem>();
    public string Address { get; set; } = null!;
}