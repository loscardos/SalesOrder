using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesOrder.Client.Services.Models;

public partial class SalesOrderItemCreateModel
{
    [Required(ErrorMessage = "Item Name is required.")]
    public string? ItemName { get; set; } 

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public double Price { get; set; }
    
    [NotMapped] public bool OnChange { get; set; } = true;
}