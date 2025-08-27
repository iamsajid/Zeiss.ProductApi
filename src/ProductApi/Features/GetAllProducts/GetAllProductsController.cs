namespace ProductApi.Features.GetAllProducts;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Common.Constants;

[ApiController]
[Tags(AppConstants.ApiTagName)]
[ApiExplorerSettings(GroupName = AppConstants.ApiGroupName)]
[Route("api/products")]
public class GetAllProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ApiExplorerSettings(GroupName = AppConstants.ApiGroupName)]
    [ProducesResponseType(typeof(GetAllProductsResponseDto), StatusCodes.Status200OK)]
    [HttpGet(Name = RouteConstants.GetAllProducts)]
    public async Task<ActionResult<GetAllProductsResponseDto>> Get([FromQuery] GetAllProductsQuery query)
    {
        var products = await _mediator.Send(query);
        return Ok(products);
    }
}
