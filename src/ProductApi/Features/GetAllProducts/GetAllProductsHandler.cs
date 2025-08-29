namespace ProductApi.Features.GetAllProducts;

using MediatR;
using ProductApi.Common.Interfaces;
using ProductApi.Domain;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
{
    private readonly IProductRepository _repo;

    public GetAllProductsHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repo.GetAllAsync();
        var productDtos = products
            .Select(p => new ProductDto(p.ProductId, p.Name, p.Category, p.Price, p.AvailableStock, p.CreatedAt))
            .ToList();

        // Filter by category
        if (!string.IsNullOrEmpty(request.Category))
            productDtos = productDtos.Where(p => p.Category.Equals(request.Category, StringComparison.OrdinalIgnoreCase)).ToList();

        // Pagination
        var totalCount = productDtos.Count;
        var pagedProducts = productDtos
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return new PagedResult<ProductDto>
        {
            Items = pagedProducts,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            PageCount = (int)Math.Ceiling((double)totalCount / request.PageSize),
            TotalCount = totalCount
        };
    }
}
