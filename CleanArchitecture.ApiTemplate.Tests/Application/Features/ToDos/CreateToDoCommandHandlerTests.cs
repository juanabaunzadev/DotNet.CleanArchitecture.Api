using CleanArchitecture.ApiTemplate.Application.Abstractions.Persistence;
using CleanArchitecture.ApiTemplate.Application.Abstractions.Repositories;
using CleanArchitecture.ApiTemplate.Application.Exceptions;
using CleanArchitecture.ApiTemplate.Application.Features.ToDos.Commands.CreateToDo;
using CleanArchitecture.ApiTemplate.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace CleanArchitecture.ApiTemplate.Tests.Application.Features.ToDos;

[TestClass]
public class CreateToDoCommandHandlerTests
{
    private IRepositoryToDo _repositoryToDo;
    private IValidator<CreateToDoCommand> _validator;
    private IUnitOfWork _unitOfWork;
    private CreateToDoCommandHandler _handler;

    [TestInitialize]
    public void Setup()
    {
        _repositoryToDo = Substitute.For<IRepositoryToDo>();
        _validator = Substitute.For<IValidator<CreateToDoCommand>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreateToDoCommandHandler(_repositoryToDo, _validator, _unitOfWork);
    }

    [TestMethod]
    public async Task Handle_ValidCommand_ShouldCreateToDo()
    {
        
        // Arrange
        var command = new CreateToDoCommand("Test ToDo", "Test Description");
        var toDo = new ToDo(command.Name, command.Description);
        
        _validator.ValidateAsync(command).Returns(new ValidationResult());
        _repositoryToDo.AddAsync(Arg.Any<ToDo>()).Returns(toDo);

        // Act
        var result = await _handler.Handle(command);

        // Assert
        await _validator.Received(1).ValidateAsync(command);
        await _repositoryToDo.Received(1).AddAsync(Arg.Any<ToDo>());
        await _unitOfWork.Received(1).CommitAsync();
        Assert.AreEqual(toDo.Id, result);
    }

    [TestMethod]
    public async Task Handle_InvalidCommand_ShouldThrowBusinessValidationException()
    {
        // Arrange
        var command = new CreateToDoCommand("", "Test Description");
        var validationResult = new ValidationResult(new[] 
        { 
            new ValidationFailure("Name", "Name is required.") 
        });

        _validator.ValidateAsync(command).Returns(validationResult);

        // Act
        await Assert.ThrowsAsync<BusinessValidationException>(() => _handler.Handle(command));

        // Assert
        await _validator.Received(1).ValidateAsync(command);
        await _repositoryToDo.DidNotReceive().AddAsync(Arg.Any<ToDo>());
    }

    [TestMethod]
    public async Task Handle_RepositoryThrowsException_ShouldRollbackUnitOfWork()
    {
        // Arrange
        var command = new CreateToDoCommand("Test ToDo", "Test Description");
        
        _validator.ValidateAsync(command).Returns(new ValidationResult());
        _repositoryToDo.AddAsync(Arg.Any<ToDo>()).Throws<Exception>();

        // Act
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command));

        // Assert
        await _validator.Received(1).ValidateAsync(command);
        await _repositoryToDo.Received(1).AddAsync(Arg.Any<ToDo>());
        await _unitOfWork.Received(1).RollbackAsync();
}
}