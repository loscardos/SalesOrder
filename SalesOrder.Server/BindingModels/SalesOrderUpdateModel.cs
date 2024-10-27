using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using SalesOrder.Shared;

namespace SalesOrder.Server.BindingModels;

public class SalesOrderUpdateModel
{
    [Required] public long SoOrderId { get; set; }

    [Required] public string? OrderNo { get; set; }

    [Required] public DateTime OrderDate { get; set; }

    [Required] public int ComCustomerId { get; set; }

    public string Address { get; set; } = null!;

    public List<SalesOrderItemUpdateModel>? SoItems { get; set; }

    public SoOrder ToEntity()
    {
        return new ()
        {
            SoOrderId = SoOrderId,
            OrderNo = OrderNo ?? string.Empty,
            OrderDate = OrderDate,
            ComCustomerId = ComCustomerId,
            Address = Address,
            SoItems = new Collection<SoItem>(
                SoItems?.Select(x => new SoItem
                {
                    SoItemId = x.SoItemId,
                    SoOrderId = x.SoOrderId,
                    ItemName = x.ItemName,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToArray()
            )
        };
    }
}