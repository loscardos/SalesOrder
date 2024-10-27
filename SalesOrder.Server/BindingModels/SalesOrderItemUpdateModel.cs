namespace SalesOrder.Server.BindingModels;

public class SalesOrderItemUpdateModel
{
    public long SoItemId { get; set; }

    public long SoOrderId { get; set; }
    
    public string? ItemName { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }
}