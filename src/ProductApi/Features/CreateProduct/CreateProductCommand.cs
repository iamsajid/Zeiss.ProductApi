namespace ProductApi.Features.CreateProduct;

using MediatR;

public record CreateProductCommand(string Name, string Category, decimal Price, int Stock) : IRequest<int>;