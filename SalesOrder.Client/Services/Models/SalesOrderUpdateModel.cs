namespace SalesOrder.Client.Services.Models;

public class SalesOrderUpdateModel
{
    public long SoOrderId { get; set; }

    public string? OrderNo { get; set; }

    public DateTime OrderDate { get; set; }

    public int ComCustomerId { get; set; }

    public string Address { get; set; } = null!;
    
    public List<SalesOrderItemUpdateModel>? SoItems { get; set; }

}