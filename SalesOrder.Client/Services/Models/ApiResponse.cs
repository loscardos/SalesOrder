namespace SalesOrder.Client.Services.Models;

public class ApiResponses<T>
{
    public List<T>? Data { get; set; }
    public Meta? Meta { get; set; }
    public string? Message { get; set; }
    public int StatusCode { set; get; }
}

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public int StatusCode { set; get; }
}

public partial class Meta
{
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
}