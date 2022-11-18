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
    Task<Result<ModelList>> List();
    
    /// <summary>
    /// Gets the model with id <paramref name="modelId" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <returns>The requested model or an error if the model could not be found.</returns>
    Task<Result<Model>> Get(string modelId);
    
    /// <summary>
    /// Creates a new model.
    /// </summary>
    /// <param name="model">The model to create.</param>
    /// <returns>The newly created model or an error if the model could not be created.</returns>
    Task<Result> Create(Model model);
    
    /// <summary>
    /// Deletes the model with id <paramref name="modelId" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <returns>Result indicating if the deletion was successful.</returns>
    Task<Result> Delete(string modelId);
}