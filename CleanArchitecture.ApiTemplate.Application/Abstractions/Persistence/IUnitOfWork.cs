namespace CleanArchitecture.ApiTemplate.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task RollbackAsync();
}