namespace ProductApi.Features.GetProductById;

using System;
using MediatR;

public record GetProductsByIdQuery(int Id) : IRequest<ProductByIdDto>;

public record ProductByIdDto(int Id, string Name, string Category, decimal Price, int AvailableStock, DateTime CreatedAt);