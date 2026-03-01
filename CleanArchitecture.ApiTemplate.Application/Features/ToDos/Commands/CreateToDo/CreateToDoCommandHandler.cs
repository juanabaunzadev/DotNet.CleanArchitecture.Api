using CleanArchitecture.ApiTemplate.Application.Abstractions.Persistence;
using CleanArchitecture.ApiTemplate.Application.Abstractions.Repositories;
using CleanArchitecture.ApiTemplate.Domain.Entities;

namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Commands.CreateToDo;

public class CreateToDoCommandHandler
{
    private readonly IRepositoryToDo _repositoryToDo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoCommandHandler(
        IRepositoryToDo repositoryToDo,
        IUnitOfWork unitOfWork)
    {
        _repositoryToDo = repositoryToDo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateToDoCommand command)
    {
        var todo = new ToDo(command.name, command.description);
        
        try
        {
            var result = await _repositoryToDo.AddAsync(todo);
            await _unitOfWork.CommitAsync();
            
            return result.Id;
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}