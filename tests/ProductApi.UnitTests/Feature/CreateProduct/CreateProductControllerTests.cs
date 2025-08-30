namespace ProductApi.UnitTests.Feature.CreateProduct;

using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Features.CreateProduct;
using MediatR;
using FluentAssertions;

public class CreateProductControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CreateProductController _controller;

    public CreateProductControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new CreateProductController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Create_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateProductCommand("Name", "Category", 10, 1);
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(123);

        // Act
        var result = await _controller.Create(command);

        // Assert
        var createdResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
        createdResult.RouteValues!["id"].Should().Be(123);
    }

    [Fact]
    public async Task Create_InvalidCommand_ReturnsBadRequest()
    {
        // Arrange
        var command = new CreateProductCommand("", "Category", 10, 1);
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(0);

        // Act
        var result = await _controller.Create(command);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }
}
