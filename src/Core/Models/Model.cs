using System.ComponentModel.DataAnnotations;

namespace DucksAndDogs.Core.Models;

/// <summary>
/// The main type for Models.
/// </summary>
/// <value></value>
public record Model
{
    /// <summary>
    /// The id of the model.
    /// </summary>
    /// <value>model-id</value>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The human-readable name of the model.
    /// </summary>
    /// <value>Duck Model</value>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The current status of the model.
    /// </summary>
    /// <value>Ready</value>
    public ModelStatus Status { get; set; } = ModelStatus.Untrained;
}
