using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

namespace SalesOrder.Shared;

public partial class ComCustomer
{
    public int ComCustomerId { get; set; }
    public string? CustomerName { get; set; }
    
    public Collection<SoOrder>? SoOrders { get; set; }
}
