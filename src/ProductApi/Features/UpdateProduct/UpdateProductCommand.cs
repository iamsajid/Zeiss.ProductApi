namespace ProductApi.Features.UpdateProduct;

using FluentValidation;
using MediatR;

public record UpdateProductCommand(int ProductId, string Name, string Category, decimal Price, int AvailableStock) : IRequest<bool>;

public class Validator : AbstractValidator<UpdateProductCommand>
{
    public Validator()
    {
        RuleFor(x => x.ProductId)
            .InclusiveBetween(100000, 999999)
            .WithMessage("Product ID must be a 6-digit number.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Product category is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be positive.");

        RuleFor(x => x.AvailableStock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product available stock must be non-negative.");
    }
}