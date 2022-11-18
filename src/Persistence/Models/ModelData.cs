namespace DucksAndDogs.Persistence.Models;

sealed record ModelData {
    public Model[] Models { get; set; } = Array.Empty<Model>();
}