namespace ProductApi.Features.UpdateProductStock;

using MediatR;

public record UpdateProductStockCommand(int ProductId, int Quantity, StockOperation Operation) : IRequest<bool>;
