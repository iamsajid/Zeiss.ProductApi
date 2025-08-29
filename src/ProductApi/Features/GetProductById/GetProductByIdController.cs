namespace ProductApi.Features.GetProductById;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Common.Constants;

[ApiController]
[Authorize]
[Tags(AppConstants.ApiTagName)]
[ApiExplorerSettings(GroupName = AppConstants.ApiGroupName)]
[Route("api/products")]
public class GetProductByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetProductByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(ProductByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = RouteConstants.GetProductById)]
    public async Task<ActionResult<ProductByIdDto>> GetProductById(int id)
    {
        var query = new GetProductsByIdQuery(id);
        var product = await _mediator.Send(query);

        if (product == null) return NotFound();

        return Ok(product);
    }
}
