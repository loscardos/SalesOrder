using SalesOrder.Server.BindingModels;
using SalesOrder.Server.DataProviders;
using SalesOrder.Server.Helper;
using SalesOrder.Shared;
using static SalesOrder.Server.BindingModels.BaseResponseModel;

namespace SalesOrder.Server.BusinessProviders.Collection;

public class SalesOrderBusinessProvider : ISalesOrderBusinessProvider
{
    private readonly ILogger<SalesOrderBusinessProvider> _logger;
    private readonly ISalesOrderDataProvider _dataProvider;

    public SalesOrderBusinessProvider(
        ILogger<SalesOrderBusinessProvider> logger,
        ISalesOrderDataProvider dataProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    }

    public async Task<Responses<SoOrder>> Get(SalesOrderListModel salesOrderListModel)
    {
        Responses<SoOrder> responses = await _dataProvider.Get(salesOrderListModel);

        if (responses.Data == null || !responses.Data.Any())
            return new Responses<SoOrder>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ApplicationConstant.NoContentMessage
            };
        
        return responses;
    }

    public async Task<Responses<ComCustomer>> GetCustomers()
    {
        Responses<ComCustomer> responses = await _dataProvider.GetCustomers();

        if (responses.Data == null || !responses.Data.Any())
            return new Responses<ComCustomer>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ApplicationConstant.NoContentMessage
            };
        
        return responses;
    }

    public async Task<Response<SoOrder>> GetById(int id)
    {
        SoOrder? soOrder = await _dataProvider.GetById(id);

        if (soOrder == null)
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = ApplicationConstant.DataNotFound
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = soOrder
        };
    }

    public async Task<Response<SoOrder>> Create(SoOrder soOrder)
    {
        SoOrder? soOrderCreate = await _dataProvider.Create(soOrder);

        if (soOrderCreate == null)
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ApplicationConstant.NotOkMessage
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = soOrder
        };
    }

    public async Task<Response<SoOrder>> Update(SoOrder soOrder)
    {
        SoOrder? soOrderUpdate = await _dataProvider.Update(soOrder);

        if (soOrderUpdate == null)
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ApplicationConstant.NotOkMessage
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = soOrder
        };
    }

    public async Task<Response<SoOrder>> Delete(int soOrderId)
    {
        SoOrder? soOrderDelete = await _dataProvider.Delete(soOrderId);

        if (soOrderDelete == null)
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ApplicationConstant.NotOkMessage
            };

        return new()
        {
            StatusCode = StatusCodes.Status204NoContent,
        };
    }
}