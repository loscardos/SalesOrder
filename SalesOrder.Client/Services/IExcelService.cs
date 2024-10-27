namespace SalesOrder.Client.Services;

public interface IExcelService
{
    public Task<byte[]> GenerateExcelWorkbook();

}