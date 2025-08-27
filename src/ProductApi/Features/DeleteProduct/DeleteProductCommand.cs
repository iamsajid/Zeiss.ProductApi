namespace ProductApi.Features.DeleteProduct;

using MediatR;

public record DeleteProductCommand(int Id) : IRequest<bool>;