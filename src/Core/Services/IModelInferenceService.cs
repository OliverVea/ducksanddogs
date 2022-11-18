using DucksAndDogs.Core.Models;

namespace DucksAndDogs.Core.Services;

/// <summary>
/// Service for making inferences with trained models.
/// </summary>
public interface IModelInferenceService
{
    /// <summary>
    /// Uses the model with id <paramref name="modelId" /> to make an inference based on the <paramref name="request" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="request"></param>
    /// <returns>An inference from the model.</returns>
    Task<Result<Inference>> Infer(string modelId, InferRequest request);
}