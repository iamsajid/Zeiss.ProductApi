namespace ProductApi.UnitTests.Feature.CreateProduct;

using FluentValidation.TestHelper;
using Moq;
using ProductApi.Common.Interfaces;
using ProductApi.Features.CreateProduct;

public class CreateProductCommandValidatorTests
{
    private readonly Mock<IProductRepository> _repoMock;
    private readonly Validator _validator;

    public CreateProductCommandValidatorTests()
    {
        _repoMock = new Mock<IProductRepository>();
        _validator = new Validator(_repoMock.Object);
    }

    [Fact]
    public async Task ValidCommand_ShouldNotHaveAnyValidationErrors()
    {
        _repoMock.Setup(r => r.ExistsByNameAsync("Unique")).ReturnsAsync(false);
        var command = new CreateProductCommand("Unique", "Category", 10, 1);
        var result = await _validator.TestValidateAsync(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Name_Empty_ShouldHaveValidationError()
    {
        var command = new CreateProductCommand("", "Category", 10, 1);
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name is required");
    }

    [Fact]
    public async Task Name_NotUnique_ShouldHaveValidationError()
    {
        _repoMock.Setup(r => r.ExistsByNameAsync("Existing")).ReturnsAsync(true);
        var command = new CreateProductCommand("Existing", "Category", 10, 1);
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Product name must be unique.");
    }

    [Fact]
    public async Task Category_Empty_ShouldHaveValidationError()
    {
        var command = new CreateProductCommand("Name", "", 10, 1);
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Category)
            .WithErrorMessage("Category is required");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task Price_NonPositive_ShouldHaveValidationError(decimal price)
    {
        var command = new CreateProductCommand("Name", "Category", price, 1);
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Price)
            .WithErrorMessage("Price must be positive");
    }

    [Theory]
    [InlineData(-5)]
    public async Task Stock_Negative_ShouldHaveValidationError(int stock)
    {
        var command = new CreateProductCommand("Name", "Category", 10, stock);
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Stock)
            .WithErrorMessage("Stock must be non-negative");
    }
}
