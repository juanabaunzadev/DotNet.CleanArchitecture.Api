using CleanArchitecture.ApiTemplate.Application.Abstractions.Persistence;
using CleanArchitecture.ApiTemplate.Application.Abstractions.Repositories;
using CleanArchitecture.ApiTemplate.Persistence.Repositories;
using CleanArchitecture.ApiTemplate.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ApiTemplate.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<ApiTemplateDbContext>(options =>
            options.UseSqlServer("name=ApiTemplateConnectionString"));

        services.AddScoped<IRepositoryToDo, RepositoryToDo>();
        services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();

        return services;
    }
}
