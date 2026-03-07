using CleanArchitecture.ApiTemplate.Application.Exceptions;
using FluentValidation;

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
        var validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());
        var validator = _serviceProvider.GetService(validatorType);

        if(validator is not null)
        {
            var validationResult = await ((dynamic)validator).ValidateAsync((dynamic)request, CancellationToken.None);
            if(!validationResult.IsValid)
            {
                throw new BusinessValidationException(validationResult);
            }
        }

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);

        if(handler is null)
        {
            throw new MediatorException($"Handler not found for {request.GetType().Name}");
        }

        return await ((dynamic)handler).Handle((dynamic)request, CancellationToken.None);
    }
}