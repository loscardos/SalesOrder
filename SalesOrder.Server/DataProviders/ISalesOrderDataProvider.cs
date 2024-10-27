using SalesOrder.Server.BindingModels;
using SalesOrder.Shared;
using static SalesOrder.Server.BindingModels.BaseResponseModel;

namespace SalesOrder.Server.DataProviders;

public interface ISalesOrderDataProvider
{
    public Task<Responses<SoOrder>> Get(SalesOrderListModel salesOrderListModel);
    
    public Task<Responses<ComCustomer>> GetCustomers();

    public Task<SoOrder?> GetById(int id);

    public Task<SoOrder?> Create(SoOrder soOrder);

    public Task<SoOrder?> Update(SoOrder soOrder);

    public Task<SoOrder?> Delete(int soOrderId);
}