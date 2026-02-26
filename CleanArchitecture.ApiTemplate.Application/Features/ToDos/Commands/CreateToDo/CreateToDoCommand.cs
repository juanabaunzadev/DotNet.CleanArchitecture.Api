namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Commands.CreateToDo;

public record CreateToDoCommand(string name, string? description);