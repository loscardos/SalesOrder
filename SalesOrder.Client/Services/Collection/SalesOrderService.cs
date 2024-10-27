using SalesOrder.Shared;

using SalesOrder.Client.Services.Models;

namespace SalesOrder.Client.Services.Collection;
public class SalesOrderService : ISalesOrderService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SalesOrderService> _logger;
    private const string BaseUrl = "https://localhost:44347/api/v1/SalesOrder"; 

    public SalesOrderService(HttpClient httpClient, ILogger<SalesOrderService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<(List<SoOrder> Orders, int? TotalRecords)> GetOrdersAsync(
        string? keywords, 
        DateTime? orderDate, 
        int page, 
        int pageSize)
    {
        try
        {
            var queryParams = new List<string>();
            
            if (!string.IsNullOrWhiteSpace(keywords))
                queryParams.Add($"Search={Uri.EscapeDataString(keywords)}");
            
            if (orderDate.HasValue)
                queryParams.Add($"OrderDate={orderDate.Value:yyyy-MM-dd}");
            
            queryParams.Add($"CurrentPage={page}");
            queryParams.Add($"PageSize={pageSize}");

            var url = $"{BaseUrl}/Get?{string.Join("&", queryParams)}";
            
            var response = await _httpClient.GetFromJsonAsync<ApiResponses<SoOrder>>(url);
            
            if (response == null)
                return (new List<SoOrder>(), 0);

            return (response.Data ?? new List<SoOrder>(), response.Meta?.TotalRecords);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching sales orders");
            throw;
        }
    }

    public async Task<List<ComCustomer>> GetComCustomerAsync()
    {
        try
        {
            var url = $"{BaseUrl}/GetCustomers";
            
            var response = await _httpClient.GetFromJsonAsync<ApiResponses<ComCustomer>>(url);
            
            if (response == null)
                return new List<ComCustomer>();

            return response.Data ?? new List<ComCustomer>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching com customer data");
            throw;
        }
    }

    public async Task<SoOrder?> GetOrderByIdAsync(long orderId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<SoOrder>>($"{BaseUrl}/GetById?id={orderId}");
            return response?.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching sales order with ID {OrderId}", orderId);
            throw;
        }
    }

    public async Task<SoOrder> CreateOrderAsync(SalesOrderCreateModel model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/Create", model);
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<SoOrder>>();
            if (result?.Data == null)
                throw new Exception("Failed to create sales order");
                
            return result.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating sales order");
            throw;
        }
    }

    public async Task<SoOrder> UpdateOrderAsync(SalesOrderUpdateModel model)
    {
        try
        {
            var response = await _httpClient.PatchAsJsonAsync($"{BaseUrl}/Update", model);
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<SoOrder>>();
            if (result?.Data == null)
                throw new Exception("Failed to update sales order");
                
            return result.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating sales order with ID {OrderId}", model.SoOrderId);
            throw;
        }
    }

    public async Task DeleteOrderAsync(long orderId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/Delete?id={orderId}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting sales order with ID {OrderId}", orderId);
            throw;
        }
    }
}