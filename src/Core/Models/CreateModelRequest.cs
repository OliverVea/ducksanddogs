using System.ComponentModel.DataAnnotations;

namespace DucksAndDogs.Core.Models;

/// <summary>
/// Used to create a Model for training and inference.
/// </summary>
public record CreateModelRequest
{
    /// <summary>
    /// The human-readable name of the model.
    /// </summary>
    /// <example>Duck Model</example>
    [Required]
    public string Name { get; set; } = string.Empty;
}
