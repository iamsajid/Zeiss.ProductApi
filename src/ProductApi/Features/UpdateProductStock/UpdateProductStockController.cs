namespace ProductApi.Features.UpdateProductStock;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Common.Constants;

[ApiController]
[Tags(AppConstants.ApiTagName)]
[ApiExplorerSettings(GroupName = AppConstants.ApiGroupName)]
[Route("api/products")]
public class UpdateProductStockController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProductStockController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpPut("add-to-stock/{productId}/{quantity}", Name = RouteConstants.AddToStock)]
    public async Task<IActionResult> UpdateStock(int productId, int quantity)
    {
        var command = new UpdateProductStockCommand(productId, quantity, StockOperation.Increase);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpPut("decrement-stock/{productId}/{quantity}", Name = RouteConstants.RemoveFromStock)]
    public async Task<IActionResult> RemoveFromStock(int productId, int quantity)
    {
        var command = new UpdateProductStockCommand(productId, quantity, StockOperation.Decrease);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
