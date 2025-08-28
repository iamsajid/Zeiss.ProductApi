namespace ProductApi.Features.DeleteProduct;

using MediatR;
using ProductApi.Common.Interfaces;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repo;

    public DeleteProductHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _repo.DeleteAsync(request.Id);
    }
}
