namespace DucksAndDogs.Core.Models;

/// <summary>
/// Contains a list of avaliable models.
/// </summary>
/// <value></value>
public record ModelList
{
    /// <summary>
    /// Used to get information about all available models.
    /// </summary>
    /// <returns>A list of avaliable models.</returns>
    public List<Model> Models { get; init; } = new List<Model>();
}