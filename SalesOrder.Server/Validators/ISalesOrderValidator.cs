using SalesOrder.Server.BindingModels;
using static SalesOrder.Server.BindingModels.BaseResponseModel;

namespace SalesOrder.Server.Validators;

public interface ISalesOrderValidator
{
    public ResponseValidator ValidateGet(SalesOrderListModel salesOrderListModel);

    public ResponseValidator ValidateGetById(int? id);

    public ResponseValidator ValidateCreate(SalesOrderCreateModel salesOrderCreateModel);

    public ResponseValidator ValidateUpdate(SalesOrderUpdateModel salesOrderUpdateModel);
}