using CleanArchitecture.ApiTemplate.Application.Exceptions;

namespace CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);

        if(handler is null)
        {
            throw new MediatorException($"Handler not found for {request.GetType().Name}");
        }

        return await ((dynamic)handler).Handle((dynamic)request);
    }
}