namespace ProductApi.Features.CreateProduct;

using MediatR;
using ProductApi.Common.Interfaces;
using ProductApi.Domain;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _repo;

    public CreateProductHandler(IProductRepository repo)
    {
        _repo = repo;
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

        await _repo.AddAsync(product);

        return product.ProductId;
    }
}
