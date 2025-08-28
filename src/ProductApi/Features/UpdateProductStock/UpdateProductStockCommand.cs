namespace ProductApi.Features.UpdateProductStock;

using FluentValidation;
using MediatR;

public record UpdateProductStockCommand(int ProductId, int Quantity, StockOperation Operation) : IRequest<bool>;

public class Validator : AbstractValidator<UpdateProductStockCommand>
{
    public Validator()
    {
        RuleFor(x => x.ProductId)
            .InclusiveBetween(100000, 999999)
            .WithMessage("Product ID must be a 6-digit number.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be positive.");

        RuleFor(x => x.Operation)
            .IsInEnum()
            .WithMessage("Invalid stock operation.");
    }
}
