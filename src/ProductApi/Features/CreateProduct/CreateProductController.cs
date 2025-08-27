namespace ProductApi.Features.CreateProduct;

using MediatR;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/products")]
public class CreateProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id }, id);
    }
}
