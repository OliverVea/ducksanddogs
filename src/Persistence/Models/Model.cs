using DucksAndDogs.Core.Models;

namespace DucksAndDogs.Persistence.Models;

sealed class Model
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ModelStatus Status { get; set; } = ModelStatus.Untrained;
}