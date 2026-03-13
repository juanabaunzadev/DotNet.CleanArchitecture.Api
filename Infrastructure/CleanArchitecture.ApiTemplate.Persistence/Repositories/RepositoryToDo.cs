using CleanArchitecture.ApiTemplate.Application.Abstractions.Repositories;
using CleanArchitecture.ApiTemplate.Domain.Entities;

namespace CleanArchitecture.ApiTemplate.Persistence.Repositories;

public class RepositoryToDo : Repository<ToDo>, IRepositoryToDo
{
    public RepositoryToDo(ApiTemplateDbContext context) : base(context)
    {
    }
}
