using CleanArchitecture.ApiTemplate.Application.Abstractions.Repositories;
using CleanArchitecture.ApiTemplate.Application.Exceptions;
using CleanArchitecture.ApiTemplate.Application.Features.ToDos.Querys.GetToDoById;
using CleanArchitecture.ApiTemplate.Domain.Entities;
using NSubstitute;

namespace CleanArchitecture.ApiTemplate.Tests.Application.Features.ToDos;

[TestClass]
public class GetToDoByIdQueryHandlerTests
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private IRepositoryToDo _repositoryToDo;
    private GetToDoByIdQueryHandler _handler;
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [TestInitialize]
    public void Initialize()
    {
        _repositoryToDo = Substitute.For<IRepositoryToDo>();
        _handler = new GetToDoByIdQueryHandler(_repositoryToDo);
    }

    [TestMethod]
    public async Task Handle_ExistingToDo_ShouldReturnToDo()
    {
        // Arrange
        var toDo = new ToDo("Test ToDo", "Test Description");
        var toDoId = toDo.Id;
        var query = new GetToDoByIdQuery(toDoId);
        
        _repositoryToDo.GetByIdAsync(toDoId).Returns(toDo);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(toDoId, result.Id);
        Assert.AreEqual(toDo.Name, result.Name);
        Assert.AreEqual(toDo.Description, result.Description);
    }

    [TestMethod]
    public async Task Handle_NonExistingToDo_ShouldThrowNotFoundException()
    {
        // Arrange
        var toDoId = Guid.NewGuid();
        var query = new GetToDoByIdQuery(toDoId);
        
        _repositoryToDo.GetByIdAsync(toDoId).Returns((ToDo)null!);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }
}
