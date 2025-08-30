namespace ProductApi.UnitTests.Feature.UpdateProductStock;

using Moq;
using ProductApi.Features.UpdateProductStock;
using ProductApi.Domain;
using Xunit;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using ProductApi.Common.Interfaces;

public class UpdateProductStockHandlerTests
{
    private readonly Mock<IProductRepository> _repoMock;
    private readonly UpdateProductStockHandler _handler;

    public UpdateProductStockHandlerTests()
    {
        _repoMock = new Mock<IProductRepository>();
        _handler = new UpdateProductStockHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_IncreaseStock_ValidQuantity_UpdatesStock()
    {
        // Arrange
        int productId = 123456;
        int initialStock = 10;
        int increaseQty = 5;
        var product = new Product { ProductId = productId, AvailableStock = initialStock };
        _repoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
        _repoMock.Setup(r => r.UpdateAsync(product)).ReturnsAsync(true);
        var command = new UpdateProductStockCommand(productId, increaseQty, StockOperation.Increase);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        product.AvailableStock.Should().Be(initialStock + increaseQty);
    }

    [Fact]
    public async Task Handle_DecreaseStock_ValidQuantity_UpdatesStock()
    {
        // Arrange
        int productId = 123456;
        int initialStock = 10;
        int decreaseQty = 5;
        var product = new Product { ProductId = productId, AvailableStock = initialStock };
        _repoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
        _repoMock.Setup(r => r.UpdateAsync(product)).ReturnsAsync(true);
        var command = new UpdateProductStockCommand(productId, decreaseQty, StockOperation.Decrease);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        product.AvailableStock.Should().Be(initialStock - decreaseQty);
    }

    [Fact]
    public async Task Handle_DecreaseStock_InsufficientStock_ThrowsException()
    {
        // Arrange
        int productId = 123456;
        int initialStock = 3;
        int decreaseQty = 5;
        var product = new Product { ProductId = productId, AvailableStock = initialStock };
        _repoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
        var command = new UpdateProductStockCommand(productId, decreaseQty, StockOperation.Decrease);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        ex.Message.Should().Be("Not enough stock to decrease");
    }

    [Fact]
    public async Task Handle_IncreaseStock_NegativeQuantity_ThrowsException()
    {
        // Arrange
        int productId = 123456;
        int initialStock = 10;
        int increaseQty = -5;
        var product = new Product { ProductId = productId, AvailableStock = initialStock };
        _repoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
        var command = new UpdateProductStockCommand(productId, increaseQty, StockOperation.Increase);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        ex.Message.Should().Be("Quantity to increase cannot be negative");
    }

    [Fact]
    public async Task Handle_InvalidProduct_ReturnsFalse()
    {
        // Arrange
        int productId = 654321;
        int decreaseQty = 5;
        _repoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(null as Product);
        var command = new UpdateProductStockCommand(productId, decreaseQty, StockOperation.Decrease);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }
}
