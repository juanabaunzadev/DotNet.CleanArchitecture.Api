using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.ApiTemplate.API.DTOs.ToDos;

public class CreateToDoDTO
{
    [Required]
    [StringLength(150)]
    public required string Name { get; set; }
    public string? Description { get; set; }
}
