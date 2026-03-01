using FluentValidation;

namespace CleanArchitecture.ApiTemplate.Application.Features.ToDos.Commands.CreateToDo;

public class CreateToDoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    public CreateToDoCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}