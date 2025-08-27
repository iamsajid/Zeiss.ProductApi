namespace ProductApi.Features.UpdateProduct;

using MediatR;

public record UpdateProductCommand(int Id, string Name, string Category, decimal Price) : IRequest<bool>;