namespace ProductApi.Features.CreateProduct;

using MediatR;

public record CreateProductCommand(string Name, decimal Price, int Stock) : IRequest<int>;