using SalesOrder.Client.Services.Models;
using SalesOrder.Shared;

namespace SalesOrder.Client.Services;

public interface ISalesOrderService
{
    Task<(List<SoOrder> Orders, int? TotalRecords)> GetOrdersAsync(string? keywords, DateTime? orderDate, int page, int pageSize);
    Task<List<ComCustomer>> GetComCustomerAsync();
    Task<SoOrder?> GetOrderByIdAsync(long orderId);
    Task<SoOrder> CreateOrderAsync(SalesOrderCreateModel model);
    Task<SoOrder> UpdateOrderAsync(SalesOrderUpdateModel model);
    Task DeleteOrderAsync(long orderId);
}