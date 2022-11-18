using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DucksAndDogs.Core.Models;

public record FileModel
{
    [Required]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public IFormFile File { get; set; } = null!;
}
