namespace ProductApi.Features.UpdateProduct;

using MediatR;
using ProductApi.Infrastructure.Data;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly ProductDbContext _context;

    public UpdateProductHandler(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync([request.Id], cancellationToken);
        if (product == null) return false;

        product.Name = request.Name;
        product.Category = request.Category;
        product.Price = request.Price;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
