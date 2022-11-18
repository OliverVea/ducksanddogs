using DucksAndDogs.Core.Models;

namespace DucksAndDogs.Core.Services;

/// <summary>
/// Service for training a model on a dataset.
/// </summary>
public interface IModelTrainingService
{
    /// <summary>
    /// Checks all models to see if any have the training status.
    /// If a model has the training status, training is commenced.
    /// </summary>
    /// <returns>Result indicating if the training was initiated successfully.</returns>
    Task<Result> TrainModelsInQueueAsync(CancellationToken cancellationToken);
}