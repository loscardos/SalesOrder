using SalesOrder.Server.BindingModels;
using SalesOrder.Shared;
using static SalesOrder.Server.BindingModels.BaseResponseModel;

namespace SalesOrder.Server.BusinessProviders;

public interface ISalesOrderBusinessProvider
{
    public Task<Responses<SoOrder>> Get(SalesOrderListModel salesOrderListModel);
    
    public Task<Responses<ComCustomer>> GetCustomers();

    public Task<Response<SoOrder>> GetById(int id);

    public Task<Response<SoOrder>> Create(SoOrder soOrder);

    public Task<Response<SoOrder>> Update(SoOrder soOrder);

    public Task<Response<SoOrder>> Delete(int soOrderId);
}