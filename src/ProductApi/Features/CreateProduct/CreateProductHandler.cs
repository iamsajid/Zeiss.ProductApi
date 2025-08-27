namespace ProductApi.Features.CreateProduct;

using MediatR;
using ProductApi.Domain;
using ProductApi.Infrastructure.Data;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly ProductDbContext _db;

    public CreateProductHandler(ProductDbContext db)
    {
        _db = db;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Category = request.Category,
            Price = request.Price,
            AvailableStock = request.Stock
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync(cancellationToken);

        return product.ProductId;
    }
}
