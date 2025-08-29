namespace ProductApi.Features.DeleteProduct;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Common.Constants;

[ApiController]
[Authorize(Roles = UserRoleType.Admin)]
[Tags(AppConstants.ApiTagName)]
[ApiExplorerSettings(GroupName = AppConstants.ApiGroupName)]
[Route("api/products")]
public class DeleteProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [HttpDelete("{id}", Name = RouteConstants.DeleteProduct)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        if (!result) return NotFound();
        return Ok(result);
    }
}
