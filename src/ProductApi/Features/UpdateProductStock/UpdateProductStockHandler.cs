namespace ProductApi.Features.UpdateProductStock;

using System;
using MediatR;
using ProductApi.Infrastructure.Data;

public class UpdateProductStockHandler : IRequestHandler<UpdateProductStockCommand, bool>
{
    private readonly ProductDbContext _context;

    public UpdateProductStockHandler(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync([request.ProductId], cancellationToken: cancellationToken);

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
                throw new Exception("Not enough stock to decrease");
            }
            product.AvailableStock -= request.Quantity;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
