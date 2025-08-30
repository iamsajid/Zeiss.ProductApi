namespace ProductApi.UnitTests.Feature.UpdateProductStock;

using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Features.UpdateProductStock;
using MediatR;
using Xunit;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;

public class UpdateProductStockControllerTests
{
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly UpdateProductStockController _controller;

    public UpdateProductStockControllerTests()
    {
        _controller = new UpdateProductStockController(_mediatorMock.Object);
    }

    [Fact]
    public async Task UpdateStock_ValidRequest_ReturnsOk()
    {
        // Arrange
        int productId = 123456;
        int quantity = 50;
        _mediatorMock.Setup(m => m.Send(It.Is<UpdateProductStockCommand>(c => c.ProductId == productId && c.Quantity == quantity), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateStock(productId, quantity);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(true);
    }

    [Fact]
    public async Task UpdateStock_InvalidProduct_ReturnsNotFound()
    {
        // Arrange
        int productId = 654321;
        int quantity = 50;
        _mediatorMock.Setup(m => m.Send(It.Is<UpdateProductStockCommand>(c => c.ProductId == productId && c.Quantity == quantity), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateStock(productId, quantity);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
