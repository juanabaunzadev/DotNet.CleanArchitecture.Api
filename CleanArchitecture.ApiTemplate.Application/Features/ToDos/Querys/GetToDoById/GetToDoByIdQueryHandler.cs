using CleanArchitecture.ApiTemplate.Application.Abstractions.Repositories;
using CleanArchitecture.ApiTemplate.Application.Exceptions;
using CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;

namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Querys.GetToDoById;

public class GetToDoByIdQueryHandler : IRequestHandler<GetToDoByIdQuery, GetToDoByIdResponse>
{
    private readonly IRepositoryToDo _repositoryToDo;

    public GetToDoByIdQueryHandler(IRepositoryToDo repositoryToDo)
    {
        _repositoryToDo = repositoryToDo;
    }

    public async Task<GetToDoByIdResponse> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
    {
        var toDo = await _repositoryToDo.GetByIdAsync(request.Id);

        if(toDo == null)
        {
            throw new NotFoundException();
        }

        return GetToDoByIdMapper.MapToResponse(toDo);
    }
}