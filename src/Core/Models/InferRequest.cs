using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DucksAndDogs.Core.Models;

/// <summary>
/// Request for inference from a model.
/// </summary>
/// <value></value>
public record InferRequest
{
    /// <summary>
    /// Image file to make inference from.
    /// </summary>
    /// <value></value>
    [Required]
    [FileExtensions(Extensions = ".png")]
    public IFormFile File { get; set; } = null!;
}
