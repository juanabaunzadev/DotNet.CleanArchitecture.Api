using CleanArchitecture.ApiTemplate.Application.Features.ToDos.Commands.CreateToDo;
using CleanArchitecture.ApiTemplate.Application.Features.ToDos.Querys.GetToDoById;
using CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ApiTemplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IMediator, Mediator>();
        services.AddScoped<IRequestHandler<GetToDoByIdQuery, GetToDoByIdResponse>, GetToDoByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateToDoCommand, Guid>, CreateToDoCommandHandler>();

        return services;
    }
}
