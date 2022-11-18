using DucksAndDogs.Core.Models;

namespace DucksAndDogs.Core.Stores;

/// <summary>
/// Used to manage model storage.
/// </summary>
public interface IModelStore
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
    /// Creates a new model.
    /// </summary>
    /// <param name="model">The model to create.</param>
    /// <returns>The newly created model or an error if the model could not be created.</returns>
    Task<Result> CreateAsync(Model model);

    /// <summary>
    /// Deletes the model with id <paramref name="modelId" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <returns>Result indicating if the deletion was successful.</returns>
    Task<Result> DeleteAsync(string modelId);

    /// <summary>
    /// Used to set status for a model. 
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="newStatus">The new status of the model.</param>
    /// <returns></returns>
    Task<Result> SetStatusAsync(string modelId, ModelStatus newStatus);
}