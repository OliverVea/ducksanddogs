namespace DucksAndDogs.Persistence.Models;

sealed class ModelData
{
    public Model[] Models { get; set; } = Array.Empty<Model>();
}