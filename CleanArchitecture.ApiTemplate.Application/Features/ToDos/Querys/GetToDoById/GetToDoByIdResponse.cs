using CleanArchitecture.ApiTemplate.Domain.Enums;

namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Querys.GetToDoById;

public class GetToDoByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ToDoStatus Status { get; set; }
}