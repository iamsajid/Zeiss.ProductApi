namespace ProductApi.Features.GetAllProducts;

using MediatR;
using ProductApi.Common.Interfaces;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, GetAllProductsResponseDto>
{
    private readonly IProductRepository _repo;

    public GetAllProductsHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<GetAllProductsResponseDto> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repo.GetAllAsync();
        var productDtos = products
            .Select(p => new ProductDto(p.ProductId, p.Name, p.Category, p.Price, p.AvailableStock, p.CreatedAt))
            .ToList();

        return new GetAllProductsResponseDto(productDtos);
    }
}
