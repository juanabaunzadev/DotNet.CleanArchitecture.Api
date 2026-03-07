using CleanArchitecture.ApiTemplate.Application.Exceptions;
using CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace CleanArchitecture.ApiTemplate.Tests.Application.Utilities.Messaging;

[TestClass]
public class MediatorTests
{
    public class TestRequest : IRequest<string> { }
    public class TestRequestHandler : IRequestHandler<TestRequest, string>
    {
        public Task<string> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Handled");
        }
    }

    [TestMethod]
    public async Task Send_ShouldCallHandleMethod()
    {
        // Arrange
        var request = new TestRequest();
        var handlerMock = Substitute.For<IRequestHandler<TestRequest, string>>();
        handlerMock.Handle(request, Arg.Any<CancellationToken>())
                   .Returns("Handled");

        var serviceProviderMock = Substitute.For<IServiceProvider>();
        serviceProviderMock.GetService(typeof(IRequestHandler<TestRequest, string>))
                           .Returns(handlerMock);

        serviceProviderMock.GetService(typeof(IValidator<TestRequest>))
                           .Returns((object)null);

        var mediator = new Mediator(serviceProviderMock);

        // Act
        var result = await mediator.Send(request);

        // Assert
        Assert.AreEqual("Handled", result);
        await handlerMock.Received(1).Handle(request, Arg.Any<CancellationToken>());
    }


    [TestMethod]
    public async Task Send_ShouldThrowMediatorException_WhenHandlerNotFound()
    {
        // Arrange
        var request = new TestRequest();
        var serviceProviderMock = Substitute.For<IServiceProvider>();

        serviceProviderMock.GetService(typeof(IRequestHandler<TestRequest, string>))
                           .Returns(null);

        var mediator = new Mediator(serviceProviderMock);

        // Act & Assert
        await Assert.ThrowsAsync<MediatorException>(() => mediator.Send(request));
    }

    [TestMethod]
    public async Task Send_ShouldThrowBusinessValidationException_WhenValidationFails()
    {
        // Arrange
        var request = new TestRequest();

        var validatorMock = Substitute.For<IValidator<TestRequest>>();
        var validationResult = new ValidationResult(new[]
        {
        new ValidationFailure("Property", "Error message")
    });
        validatorMock.ValidateAsync(request, Arg.Any<CancellationToken>())
                     .Returns(validationResult);

        var handlerMock = Substitute.For<IRequestHandler<TestRequest, string>>();

        var serviceProviderMock = Substitute.For<IServiceProvider>();
        serviceProviderMock.GetService(typeof(IValidator<TestRequest>))
                           .Returns(validatorMock);
        serviceProviderMock.GetService(typeof(IRequestHandler<TestRequest, string>))
                           .Returns(handlerMock);

        var mediator = new Mediator(serviceProviderMock);

        // Act & Assert
        await Assert.ThrowsAsync<BusinessValidationException>(() => mediator.Send(request));
        await handlerMock.DidNotReceive().Handle(request, Arg.Any<CancellationToken>());
    }
}