namespace SalesOrder.Server.BindingModels;

public partial class SalesOrderItemCreateModel
{
    public string? ItemName { get; set; } 

    public int Quantity { get; set; }

    public double Price { get; set; }
}