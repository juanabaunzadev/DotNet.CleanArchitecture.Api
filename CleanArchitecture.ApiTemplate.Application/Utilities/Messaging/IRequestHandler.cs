namespace CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;

public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> 
{
    Task<TResponse> Handle(TRequest request);
}