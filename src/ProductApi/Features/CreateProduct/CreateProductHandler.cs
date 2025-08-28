namespace ProductApi.Features.CreateProduct;

using MediatR;
using ProductApi.Common.Interfaces;
using ProductApi.Domain;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _repo;
    private readonly IProductIdGenerator _idGenerator;

    public CreateProductHandler(IProductRepository repo, IProductIdGenerator idGenerator)
    {
        _repo = repo;
        _idGenerator = idGenerator;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productId = await _idGenerator.GenerateUniqueProductIdAsync();
        var product = new Product
        {
            ProductId = productId,
            Name = request.Name,
            Category = request.Category,
            Price = request.Price,
            AvailableStock = request.Stock
        };

        await _repo.AddAsync(product);

        return product.ProductId;
    }
}
