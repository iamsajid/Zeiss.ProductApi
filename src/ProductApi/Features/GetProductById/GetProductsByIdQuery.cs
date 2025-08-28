namespace ProductApi.Features.GetProductById;

using System;
using FluentValidation;
using MediatR;

public record GetProductsByIdQuery(int Id) : IRequest<ProductByIdDto>;

public record ProductByIdDto(int Id, string Name, string Category, decimal Price, int AvailableStock, DateTime CreatedAt);

public class Validator : AbstractValidator<GetProductsByIdQuery>
{
    public Validator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(100000, 999999)
            .WithMessage("Product ID must be a 6-digit number.");
    }
}