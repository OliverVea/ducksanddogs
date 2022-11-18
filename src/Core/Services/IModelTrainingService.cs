using DucksAndDogs.Core.Models;

namespace DucksAndDogs.Core.Services;

/// <summary>
/// Service for training a model on a dataset.
/// </summary>
public interface IModelTrainingService
{
    /// <summary>
    /// Starts training the model with id <paramref name="modelId" /> with the parameters specified in <paramref name="request" />.
    /// The model is ready when it has the 'Ready' status.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="request"></param>
    /// <returns>Result indicating if the training was initiated successfully.</returns>
    Task<Result> Train(string modelId, TrainModelRequest request);
}