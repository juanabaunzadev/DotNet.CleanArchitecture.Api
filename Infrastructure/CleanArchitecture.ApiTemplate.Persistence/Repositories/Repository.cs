using CleanArchitecture.ApiTemplate.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.ApiTemplate.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApiTemplateDbContext _context;

    public Repository(ApiTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public Task<T> Add(T entity)
    {
        _context.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<T> Update(T entity)
    {
        _context.Update(entity);
        return Task.FromResult(entity);
    }

    public Task Delete(T entity)
    {
        _context.Remove(entity);
        return Task.CompletedTask;
    }
}
