namespace CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;

public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
}