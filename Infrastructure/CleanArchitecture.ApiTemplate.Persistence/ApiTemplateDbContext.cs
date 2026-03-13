using CleanArchitecture.ApiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.ApiTemplate.Persistence;

public class ApiTemplateDbContext : DbContext
{
    public ApiTemplateDbContext(DbContextOptions<ApiTemplateDbContext> options) : base(options)
    {
    }

    protected ApiTemplateDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiTemplateDbContext).Assembly);
    }

    public DbSet<ToDo> ToDos { get; set; }
}
