using SalesOrder.Server.BindingModels;
using SalesOrder.Server.Helper;
using static SalesOrder.Server.BindingModels.BaseResponseModel;

namespace SalesOrder.Server.Validators.Collection;

public class SalesOrderValidator : ISalesOrderValidator
{
    public ResponseValidator ValidateGet(SalesOrderListModel salesOrderListModel)
    {
        ResponseValidator responseValidator = new() { IsValid = true };
        
        // validator goes here
        
        return responseValidator;
    }

    public ResponseValidator ValidateGetById(int? id)
    {
        ResponseValidator responseValidator = new() { IsValid = true };

        if (id == null)
            return new() { Message = string.Format(ApplicationConstant.DataNotFound, nameof(id)) };

        return responseValidator;
    }

    public ResponseValidator ValidateCreate(SalesOrderCreateModel salesOrderCreateModel)
    {
        ResponseValidator responseValidator = new() { IsValid = true };

        return responseValidator;
    }

    public ResponseValidator ValidateUpdate(SalesOrderUpdateModel salesOrderUpdateModel)
    {
        ResponseValidator responseValidator = new() { IsValid = true };

        return responseValidator;
    }
}