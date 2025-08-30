namespace ProductApi.UnitTests.Feature.UpdateProductStock;

using FluentValidation.TestHelper;
using ProductApi.Features.UpdateProductStock;
using Xunit;

public class UpdateProductStockCommandValidatorTests
{
    private readonly Validator _validator = new();

    [Fact]
    public void ValidCommand_ShouldNotHaveAnyValidationErrors()
    {
        var result = _validator.TestValidate(new UpdateProductStockCommand(123456, 10, StockOperation.Increase));
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(12345)]
    [InlineData(1234567)]
    public void ProductId_Invalid_ShouldHaveValidationError(int productId)
    {
        var result = _validator.TestValidate(new UpdateProductStockCommand(productId, 10, StockOperation.Decrease));
        result.ShouldHaveValidationErrorFor(c => c.ProductId)
            .WithErrorMessage("Product ID must be a 6-digit number.");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Quantity_Negative_ShouldHaveValidationError(int quantity)
    {
        var result = _validator.TestValidate(new UpdateProductStockCommand(123456, quantity, StockOperation.Decrease));
        result.ShouldHaveValidationErrorFor(c => c.Quantity)
            .WithErrorMessage("Quantity must be positive.");
    }
}
