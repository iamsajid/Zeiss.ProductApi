namespace ProductApi.UnitTests.Feature.DeleteProduct;

using Moq;
using ProductApi.Features.DeleteProduct;
using Xunit;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using ProductApi.Common.Interfaces;

public class DeleteProductHandlerTests
{
    private readonly Mock<IProductRepository> _repoMock;
    private readonly DeleteProductHandler _handler;

    public DeleteProductHandlerTests()
    {
        _repoMock = new Mock<IProductRepository>();
        _handler = new DeleteProductHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ExistingProduct_ReturnsTrue()
    {
        // Arrange
        int productId = 123456;
        _repoMock.Setup(r => r.DeleteAsync(productId)).ReturnsAsync(true);
        var command = new DeleteProductCommand(productId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_NonExistingProduct_ReturnsFalse()
    {
        // Arrange
        int productId = 654321;
        _repoMock.Setup(r => r.DeleteAsync(productId)).ReturnsAsync(false);
        var command = new DeleteProductCommand(productId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }
}
