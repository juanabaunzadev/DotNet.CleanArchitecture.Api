using FluentValidation.Results;

namespace CleanArchitecture.ApiTemplate.Application.Exceptions;

public class BusinessValidationException : Exception
{
    public List<string> Errors { get; set; } = new List<string>();

    public BusinessValidationException(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Errors.Add(error.ErrorMessage);
        }
    }
}