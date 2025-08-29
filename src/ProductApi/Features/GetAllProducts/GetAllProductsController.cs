namespace ProductApi.Features.GetAllProducts;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Common.Constants;
using ProductApi.Domain;

[ApiController]
[Tags(AppConstants.ApiTagName)]
[ApiExplorerSettings(GroupName = AppConstants.ApiGroupName)]
[Route("api/products")]
public class GetAllProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public GetAllProductsController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [ApiExplorerSettings(GroupName = AppConstants.ApiGroupName)]
    [ProducesResponseType(typeof(PagedResult<ProductDto>), StatusCodes.Status200OK)]
    [HttpGet(Name = RouteConstants.GetAllProducts)]
    public async Task<ActionResult<PagedResult<ProductDto>>> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var defaultPageNumber = _configuration.GetValue<int>("Pagination:DefaultPageNumber");
        var defaultPageSize = _configuration.GetValue<int>("Pagination:DefaultPageSize");

        var query = new GetAllProductsQuery(pageNumber ?? defaultPageNumber, pageSize ?? defaultPageSize);
        var products = await _mediator.Send(query);

        if (products.PageCount > 0 && (pageNumber ?? defaultPageNumber) > products.PageCount)
            return BadRequest($"Page number {(pageNumber ?? defaultPageNumber)} is out of range. Maximum page is {products.PageCount}.");
        
        return Ok(products);
    }
}
