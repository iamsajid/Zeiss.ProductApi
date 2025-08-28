namespace ProductApi.Features.UpdateProduct;

using MediatR;
using ProductApi.Common.Interfaces;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _repo;

    public UpdateProductHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repo.GetByIdAsync(request.Id);
        if (product == null) return false;

        product.Name = request.Name;
        product.Category = request.Category;
        product.Price = request.Price;
        product.AvailableStock = request.AvailableStock;

        await _repo.UpdateAsync(product);
        return true;
    }
}
