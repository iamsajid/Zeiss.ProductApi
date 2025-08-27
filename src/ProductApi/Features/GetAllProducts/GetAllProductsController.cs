namespace ProductApi.Features.GetAllProducts;

using MediatR;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class GetAllProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetAllProductsResponseDto>> Get([FromQuery] GetAllProductsQuery query)
    {
        var products = await _mediator.Send(query);
        return Ok(products);
    }
}
