namespace ProductApi.Features.CreateProduct;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Common.Constants;

[ApiController]
[Authorize(Roles = UserRoleType.Admin)]
[Tags(AppConstants.ApiTagName)]
[ApiExplorerSettings(GroupName = AppConstants.ApiGroupName)]
[Route("api/products")]
public class CreateProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [HttpPost(Name = RouteConstants.CreateProduct)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtRoute(RouteConstants.GetProductById, new { id }, id);
    }
}