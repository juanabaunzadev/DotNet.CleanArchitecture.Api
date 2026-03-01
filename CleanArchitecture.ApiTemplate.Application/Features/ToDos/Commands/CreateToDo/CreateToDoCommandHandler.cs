using CleanArchitecture.ApiTemplate.Application.Abstractions.Repositories;
using CleanArchitecture.ApiTemplate.Domain.Entities;

namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Commands.CreateToDo;

public class CreateToDoCommandHandler
{
    private readonly IRepositoryToDo _repositoryToDo;

    public CreateToDoCommandHandler(IRepositoryToDo repositoryToDo)
    {
        _repositoryToDo = repositoryToDo;
    }

    public async Task<Guid> Handle(CreateToDoCommand command)
    {
        var todo = new ToDo(command.name, command.description);
        var result = await _repositoryToDo.AddAsync(todo);

        return result.Id;
    }
}