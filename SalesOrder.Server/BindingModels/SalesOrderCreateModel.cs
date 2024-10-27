using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using SalesOrder.Shared;

namespace SalesOrder.Server.BindingModels;

public class SalesOrderCreateModel
{
    [Required] public string? OrderNo { get; set; }

    [Required] public DateTime OrderDate { get; set; }

    public int ComCustomerId { get; set; }

    public string Address { get; set; } = null!;

    public List<SalesOrderItemCreateModel>? SoItems { get; set; }

    public SoOrder ToEntity()
    {
        return new ()
        {
            OrderNo = OrderNo ?? string.Empty,
            OrderDate = OrderDate,
            ComCustomerId = ComCustomerId,
            Address = Address,
            SoItems = new Collection<SoItem>(
                SoItems?.Select(x => new SoItem
                {
                    ItemName = x.ItemName,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToArray()
            )
        };
    }
}