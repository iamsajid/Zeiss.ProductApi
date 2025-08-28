namespace ProductApi.Features.CreateProduct;

using FluentValidation;
using MediatR;
using ProductApi.Common.Interfaces;

public record CreateProductCommand(string Name, string Category, decimal Price, int Stock) : IRequest<int>;

public class Validator : AbstractValidator<CreateProductCommand>
{
    public Validator(IProductRepository repo)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MustAsync(async (name, cancellation) =>
            {
                return !await repo.ExistsByNameAsync(name);
            })
            .WithMessage("Product name must be unique.");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be positive");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock must be non-negative");
    }
}