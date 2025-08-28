namespace ProductApi.Features.DeleteProduct;

using FluentValidation;
using MediatR;

public record DeleteProductCommand(int Id) : IRequest<bool>;

public class Validator : AbstractValidator<DeleteProductCommand>
{
    public Validator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(100000, 999999)
            .WithMessage("Product ID must be a 6-digit number.");
    }
}