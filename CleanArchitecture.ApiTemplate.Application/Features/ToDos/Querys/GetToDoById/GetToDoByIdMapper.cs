using CleanArchitecture.ApiTemplate.Domain.Entities;

namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Querys.GetToDoById;

public static class GetToDoByIdMapper
{
    public static GetToDoByIdResponse MapToResponse(ToDo toDo)
    {
        return new GetToDoByIdResponse
        {
            Id = toDo.Id,
            Name = toDo.Name,
            Description = toDo.Description,
            Status = toDo.Status
        };
    }
}
