namespace ProductApi.UnitTests.Feature.DeleteProduct;

using FluentValidation.TestHelper;
using ProductApi.Features.DeleteProduct;
using Xunit;

public class DeleteProductCommandValidatorTests
{
    private readonly Validator _validator = new();

    [Theory]
    [InlineData(123456)]
    [InlineData(654321)]
    public void ValidProductId_ShouldNotHaveAnyValidationErrors(int productId)
    {
        var result = _validator.TestValidate(new DeleteProductCommand(productId));
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(12345)]
    [InlineData(1234567)]
    [InlineData(0)]
    public void ProductId_Invalid_ShouldHaveValidationError(int productId)
    {
        var result = _validator.TestValidate(new DeleteProductCommand(productId));
        result.ShouldHaveValidationErrorFor(c => c.ProductId)
            .WithErrorMessage("Product ID must be a 6-digit number.");
    }
}
