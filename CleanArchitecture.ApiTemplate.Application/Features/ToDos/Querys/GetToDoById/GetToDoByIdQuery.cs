using CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;

namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Querys.GetToDoById;

public record GetToDoByIdQuery(Guid Id) : IRequest<GetToDoByIdResponse>;