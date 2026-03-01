using CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;

namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Commands.CreateToDo;

public record CreateToDoCommand(string Name, string? Description) : IRequest<Guid>;