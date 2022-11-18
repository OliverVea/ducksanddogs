using DucksAndDogs.Core.Models;

namespace DucksAndDogs.Core.Services;

/// <summary>
/// Used to manage, train and infer using models.
/// </summary>
public interface IModelService
{
    /// <summary>
    /// Lists avaliable models.
    /// </summary>
    /// <returns>A ModelList object containing the available models.</returns>
    Task<Result<ModelList>> ListAsync();

    /// <summary>
    /// Gets the model with id <paramref name="modelId" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <returns>The requested model or an error if the model could not be found.</returns>
    Task<Result<Model>> GetAsync(string modelId);

    /// <summary>
    /// Creates a new model with id <paramref name="modelId" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="request"></param>
    /// <returns>The newly created model or an error if the model could not be created.</returns>
    Task<Result<Model>> CreateAsync(string modelId, CreateModelRequest request);

    /// <summary>
    /// Deletes the model with id <paramref name="modelId" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <returns>Result indicating if the deletion was successful.</returns>
    Task<Result> DeleteAsync(string modelId);

    /// <summary>
    /// Used to set status for a model. 
    /// This can be used to queue the model for training or indicate that the model is done training.
    /// Will return an error if the model is transferred directly from Untrained to Ready.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="newStatus">The new status of the model.</param>
    /// <returns></returns>
    Task<Result> SetStatusAsync(string modelId, ModelStatus newStatus);
}
