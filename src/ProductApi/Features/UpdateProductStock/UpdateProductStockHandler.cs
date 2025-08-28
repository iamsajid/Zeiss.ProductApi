namespace ProductApi.Features.UpdateProductStock;

using System;
using MediatR;
using ProductApi.Common.Interfaces;

public class UpdateProductStockHandler : IRequestHandler<UpdateProductStockCommand, bool>
{
    private readonly IProductRepository _repo;

    public UpdateProductStockHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _repo.GetByIdAsync(request.ProductId);

        if (product == null)
        {
            return false;
        }

        if (request.Operation == StockOperation.Increase)
        {
            if (request.Quantity < 0)
            {
                throw new Exception("Quantity to increase cannot be negative");
            }
            product.AvailableStock += request.Quantity;
        }

        if (request.Operation == StockOperation.Decrease)
        {
            if (product.AvailableStock < request.Quantity)
            {
                // TODO: Create a new AppException to show logical error and handle separately in middleware
                throw new Exception("Not enough stock to decrease");
            }
            product.AvailableStock -= request.Quantity;
        }

        await _repo.UpdateAsync(product);

        return true;
    }
}
