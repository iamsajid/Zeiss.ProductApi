#nullable enable
namespace ProductApi.Features.GetAllProducts;

using FluentValidation;
using MediatR;
using ProductApi.Domain;

public record GetAllProductsQuery(int PageNumber, int PageSize, string? Category = null) : IRequest<PagedResult<ProductDto>>;

public record ProductDto(int Id, string Name, string Category, decimal Price, int AvailableStock, DateTime CreatedAt);

public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
{
    public GetAllProductsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 50)
            .WithMessage("PageSize must be between 1 and 50.");
    }
}