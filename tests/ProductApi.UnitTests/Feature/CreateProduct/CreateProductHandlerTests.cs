namespace ProductApi.UnitTests.Feature.CreateProduct;

using FluentAssertions;
using Moq;
using ProductApi.Common.Interfaces;
using ProductApi.Features.CreateProduct;
using ProductApi.Domain;

public class CreateProductHandlerTests
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<IProductIdGenerator> _idGenerator;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _productRepository = new Mock<IProductRepository>();
        _idGenerator = new Mock<IProductIdGenerator>();
        _handler = new CreateProductHandler(_productRepository.Object, _idGenerator.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateProductAndReturnId()
    {
        // Arrange
        var expectedId = 123456;
        _idGenerator.Setup(x => x.GenerateUniqueProductIdAsync()).ReturnsAsync(expectedId);
        _productRepository.Setup(x => x.AddAsync(It.IsAny<Product>())).ReturnsAsync(expectedId);

        var command = new CreateProductCommand("Test Product", "Test Category", 99.99m, 10);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(expectedId);
        _productRepository.Verify(x => x.AddAsync(It.Is<Product>(p =>
            p.ProductId == expectedId &&
            p.Name == "Test Product" &&
            p.Category == "Test Category" &&
            p.Price == 99.99m &&
            p.AvailableStock == 10
        )), Times.Once);
        _idGenerator.Verify(x => x.GenerateUniqueProductIdAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        _idGenerator.Setup(x => x.GenerateUniqueProductIdAsync()).ReturnsAsync(1);
        _productRepository.Setup(x => x.AddAsync(It.IsAny<Product>())).ThrowsAsync(new Exception("DB error"));

        var command = new CreateProductCommand("Test Product", "Test Category", 99.99m, 10);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("DB error");
    }
}
