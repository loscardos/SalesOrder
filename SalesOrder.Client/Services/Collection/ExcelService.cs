using OfficeOpenXml;
using SalesOrder.Client.Services.Collection;

namespace SalesOrder.Client.Services;

public class ExcelService : IExcelService
{
    private readonly ISalesOrderService _salesOrderService;

    public ExcelService(ISalesOrderService salesOrderService)
    {
        _salesOrderService = salesOrderService;
    }

    public async Task<byte[]> GenerateExcelWorkbook()
    {
        var stream = new MemoryStream();
        
        var salesOrder = await _salesOrderService.GetOrdersAsync(null, null, 0, 0);

        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            workSheet.Cells.LoadFromCollection(salesOrder.Orders, true);

            return package.GetAsByteArray();
        }
    }
}