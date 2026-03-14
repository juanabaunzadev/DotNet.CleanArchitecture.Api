using CleanArchitecture.ApiTemplate.API.DTOs.ToDos;
using CleanArchitecture.ApiTemplate.Application.Features.ToDos.Commands.CreateToDo;
using CleanArchitecture.ApiTemplate.Application.Features.ToDos.Querys.GetToDoById;
using CleanArchitecture.ApiTemplate.Application.Utilities.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.ApiTemplate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetToDoByIdResponse>> GetById(Guid id)
    {
        var query = new GetToDoByIdQuery(id);
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateToDoDTO createToDoDto)
    {
        var command = new CreateToDoCommand(createToDoDto.Name, createToDoDto.Description);
        await _mediator.Send(command);

        return Ok();
    }
}
