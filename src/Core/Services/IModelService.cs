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

    /// <summary>
    /// Starts training the model with id <paramref name="modelId" /> with the parameters specified in <paramref name="request" />.
    /// The model is ready when it has the 'Ready' status.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="request"></param>
    /// <returns>Result indicating if the training was initiated successfully.</returns>
    Task<Result> Train(string modelId, TrainModelRequest request);

    /// <summary>
    /// Uses the model with id <paramref name="modelId" /> to make an inference based on the <paramref name="request" />.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="request"></param>
    /// <returns>An inference from the model.</returns>
    Task<Result<Inference>> Infer(string modelId, InferRequest request);
}
