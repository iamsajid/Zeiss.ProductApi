namespace ProductApi.UnitTests.Feature.DeleteProduct;

using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Features.DeleteProduct;
using MediatR;
using FluentAssertions;

public class DeleteProductControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly DeleteProductController _controller;

    public DeleteProductControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new DeleteProductController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Delete_ExistingProduct_ReturnsOk()
    {
        // Arrange
        int productId = 123456;
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteProductCommand>(c => c.ProductId == productId), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(productId);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(true);
    }

    [Fact]
    public async Task Delete_NonExistingProduct_ReturnsNotFound()
    {
        // Arrange
        int productId = 654321;
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteProductCommand>(c => c.ProductId == productId), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(productId);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
