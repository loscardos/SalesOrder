using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using SalesOrder.Shared;

namespace SalesOrder.Client.Services.Models;

public class SalesOrderCreateModel
{
    [Required(ErrorMessage = "Order No is required.")]
    public string? OrderNo { get; set; }

    [Required(ErrorMessage = "Order Date is required.")]
    public DateTime OrderDate { get; set; }

    public int ComCustomerId { get; set; }

    public string Address { get; set; } = null!;

    public List<SalesOrderItemCreateModel>? SoItems { get; set; }
}