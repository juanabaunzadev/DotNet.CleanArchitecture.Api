using CleanArchitecture.ApiTemplate.Application.Abstractions.Persistence;
using CleanArchitecture.ApiTemplate.Application.Abstractions.Repositories;
using CleanArchitecture.ApiTemplate.Application.Exceptions;
using CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;
using CleanArchitecture.ApiTemplate.Domain.Entities;
using FluentValidation;

namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Commands.CreateToDo;

public class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand, Guid>
{
    private readonly IRepositoryToDo _repositoryToDo;
    private readonly IValidator<CreateToDoCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoCommandHandler(
        IRepositoryToDo repositoryToDo,
        IValidator<CreateToDoCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _repositoryToDo = repositoryToDo;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateToDoCommand command)
    {
        var validationResult = await _validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new BusinessValidationException(validationResult);
        }

        var todo = new ToDo(command.Name, command.Description);
        
        try
        {
            var result = await _repositoryToDo.AddAsync(todo);
            await _unitOfWork.CommitAsync();
            
            return result.Id;
        }
        catch(Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}