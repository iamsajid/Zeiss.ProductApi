using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductApi.Features.CreateProduct;
using ProductApi.Infrastructure.Data;

public class CreateProductHandlerTests
{
    private ProductDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: "ProductApiTestDb")
            .Options;
        return new ProductDbContext(options);
    }

    [Fact]
    public async Task Handle_ShouldCreateProductAndReturnId()
    {
        // Arrange
        var dbContext = GetDbContext();
        var handler = new CreateProductHandler(dbContext);
        var command = new CreateProductCommand("Test Product", "Test Category", 99.99m, 10);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeGreaterThan(0);
        var product = await dbContext.Products.FindAsync(result);
        product.Should().NotBeNull();
        product.Name.Should().Be("Test Product");
        product.Category.Should().Be("Test Category");
        product.Price.Should().Be(99.99m);
    }

    [Fact]
    public async Task Handle_ShouldNotCreateProduct_WhenNameIsNullOrEmpty()
    {
        // Arrange
        var dbContext = GetDbContext();
        var handler = new CreateProductHandler(dbContext);
        var command = new CreateProductCommand("", "Test Category", 99.99m, 10);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(0);
    }
}
