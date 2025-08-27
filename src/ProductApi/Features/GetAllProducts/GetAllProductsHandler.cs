namespace ProductApi.Features.GetAllProducts;

using MediatR;
using ProductApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, GetAllProductsResponseDto>
{
    private readonly ProductDbContext _context;

    public GetAllProductsHandler(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<GetAllProductsResponseDto> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var productDtos = await _context.Products
            .Select(p => new ProductDto(p.ProductId, p.Name, p.Category, p.Price))
            .ToListAsync(cancellationToken);

        return new GetAllProductsResponseDto(productDtos);
    }
}
