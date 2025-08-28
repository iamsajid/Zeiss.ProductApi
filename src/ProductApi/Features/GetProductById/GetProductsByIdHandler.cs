namespace ProductApi.Features.GetProductById;

using MediatR;
using ProductApi.Infrastructure.Data;

public class GetProductsByIdHandler : IRequestHandler<GetProductsByIdQuery, ProductByIdDto>
{
    private readonly ProductDbContext _context;

    public GetProductsByIdHandler(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<ProductByIdDto> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync([request.Id], cancellationToken: cancellationToken);
        if (product == null) return null;

        return new ProductByIdDto(product.ProductId, product.Name, product.Category, product.Price, product.AvailableStock, product.CreatedAt);
    }
}
