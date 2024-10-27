using Microsoft.AspNetCore.Mvc;
using SalesOrder.Server.BindingModels;
using SalesOrder.Server.BusinessProviders;
using SalesOrder.Server.Validators;
using SalesOrder.Shared;
using static SalesOrder.Server.BindingModels.BaseResponseModel;

namespace SalesOrder.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class SalesOrderController : ControllerBase
{
    private readonly ILogger<SalesOrderController> _logger;
    private readonly ISalesOrderBusinessProvider _businessProvider;
    private readonly ISalesOrderValidator _validator;

    public SalesOrderController(
        ILogger<SalesOrderController> logger,
        ISalesOrderBusinessProvider businessProvider,
        ISalesOrderValidator validator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _businessProvider = businessProvider ?? throw new ArgumentNullException(nameof(businessProvider));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    [HttpGet]
    [ProducesResponseType(typeof(Responses<SoOrder>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] SalesOrderListModel salesOrderParamModel)
    {
        try
        {
            ResponseValidator responseValidator = _validator.ValidateGet(salesOrderParamModel);

            if (!responseValidator.IsValid)
                return BadRequest(new Responses()
                    { Message = responseValidator.Message });
            
            Responses<SoOrder> responses = await _businessProvider.Get(salesOrderParamModel);
                
            return StatusCode(responses.StatusCode, responses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Responses
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(Responses<SoOrder>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomers()
    {
        try
        {
            Responses<ComCustomer> responses = await _businessProvider.GetCustomers();
                
            return StatusCode(responses.StatusCode, responses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Responses
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<SoOrder>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            ResponseValidator responseValidationModel = _validator.ValidateGetById(id);

            if (!responseValidationModel.IsValid)
                return BadRequest(new ResponseValidator { Message = responseValidationModel.Message });

            Response<SoOrder> response = await _businessProvider.GetById(id);

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<SoOrder>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response<SoOrder>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] SalesOrderCreateModel salesOrderCreateModel)
    {
        try
        {
            ResponseValidator responseValidationModel = _validator.ValidateCreate(salesOrderCreateModel);

            if (!responseValidationModel.IsValid)
                return BadRequest(new ResponseValidator { Message = responseValidationModel.Message });

            Response<SoOrder> response = await _businessProvider.Create(salesOrderCreateModel.ToEntity());

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<SoOrder>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpPatch]
    [ProducesResponseType(typeof(Response<SoOrder>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] SalesOrderUpdateModel salesOrderUpdateModel)
    {
        try
        {
            ResponseValidator responseValidationModel = _validator.ValidateUpdate(salesOrderUpdateModel);

            if (!responseValidationModel.IsValid)
                return BadRequest(new ResponseValidator { Message = responseValidationModel.Message });

            Response<SoOrder> response = await _businessProvider.Update(salesOrderUpdateModel.ToEntity());

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<SoOrder>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }
    
    [HttpDelete]
    [ProducesResponseType(typeof(Response<SoOrder>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            ResponseValidator responseValidationModel = _validator.ValidateGetById(id);

            if (!responseValidationModel.IsValid)
                return BadRequest(new ResponseValidator { Message = responseValidationModel.Message });

            Response<SoOrder> response = await _businessProvider.Delete(id);

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<SoOrder>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }
}