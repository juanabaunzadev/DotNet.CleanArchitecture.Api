using CleanArchitecture.ApiTemplate.Application.Exceptions;
using CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;
using NSubstitute;

namespace CleanArchitecture.ApiTemplate.Tests.Application.Utilities.Messaging;

[TestClass]
public class MediatorTests
{
    public class TestRequest : IRequest<string> { }
    public class TestRequestHandler : IRequestHandler<TestRequest, string>
    {
        public Task<string> Handle(TestRequest request)
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
        var serviceProviderMock = Substitute.For<IServiceProvider>();
        
        serviceProviderMock.GetService(typeof(IRequestHandler<TestRequest, string>))
                           .Returns(handlerMock);

        var mediator = new Mediator(serviceProviderMock);

        // Act
        var result = await mediator.Send(request);

        // Assert
        await handlerMock.Received(1).Handle(request);
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
}