namespace ProductApi.Features.GetAllProducts;

using MediatR;

// TODO: add pagination properties
public record GetAllProductsQuery() : IRequest<GetAllProductsResponseDto>;

public record GetAllProductsResponseDto(List<ProductDto> Products);

public record ProductDto(int Id, string Name, string Description, decimal Price);
