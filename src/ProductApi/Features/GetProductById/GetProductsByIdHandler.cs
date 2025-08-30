namespace ProductApi.Features.GetProductById;

using MediatR;
using ProductApi.Common.Interfaces;

public class GetProductsByIdHandler : IRequestHandler<GetProductsByIdQuery, ProductByIdDto>
{
    private readonly IProductRepository _repo;

    public GetProductsByIdHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<ProductByIdDto> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repo.GetByIdAsync(request.ProductId);
        if (product == null) return null;

        return new ProductByIdDto(product.ProductId, product.Name, product.Category, product.Price, product.AvailableStock, product.CreatedAt);
    }
}
