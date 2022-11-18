using DucksAndDogs.Core.Models;

namespace DucksAndDogs.Persistence.Models;

sealed record Model
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public ModelStatus Status { get; init; } = ModelStatus.Untrained;
}