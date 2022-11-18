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
    Task<Result<ModelList>> List();

    /// <summary>
    /// Gets the model with id <paramref name="modelId" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <returns>The requested model or an error if the model could not be found.</returns>
    Task<Result<Model>> Get(string modelId);

    /// <summary>
    /// Creates a new model with id <paramref name="modelId" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="request"></param>
    /// <returns>The newly created model or an error if the model could not be created.</returns>
    Task<Result<Model>> Create(string modelId, CreateModelRequest request);

    /// <summary>
    /// Deletes the model with id <paramref name="modelId" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <returns>Result indicating if the deletion was successful.</returns>
    Task<Result> Delete(string modelId);
}
