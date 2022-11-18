using DucksAndDogs.Core.Models;
using DucksAndDogs.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DucksAndDogs.Api.Controllers;

/// <summary>
/// Controller for handling models used for inference.
/// </summary>
[ApiController]
[Route("models")]
public class ModelsController : ControllerBase
{
    private readonly IModelService _modelService;
    private readonly IModelTrainingService _trainingService;
    private readonly IModelInferenceService _inferenceService;

    /// <summary>
    /// Constructor called by the framework.
    /// </summary>
    /// <param name="modelService"></param>
    /// <param name="trainingService"></param>
    /// <param name="inferenceService"></param>
    public ModelsController(IModelService modelService, IModelTrainingService trainingService, IModelInferenceService inferenceService)
    {
        _modelService = modelService;
        _trainingService = trainingService;
        _inferenceService = inferenceService;
    }

    /// <summary>
    /// Lists all available models.
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "ListModels")]
    public async Task<ActionResult<ModelList>> List()
    {
        var result = await _modelService.List();
        if (result.Succeeded()) return result.Value;
        return MapError(result.Error);
    }

    /// <summary>
    /// Gets information on a specific model.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <returns></returns>
    [HttpGet("{modelId}", Name = "GetModel")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Model>> Get(string modelId)
    {
        var result = await _modelService.Get(modelId);
        if (result.Succeeded()) return result.Value;
        return MapError(result.Error);
    }

    /// <summary>
    /// Registers a model for training.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{modelId}", Name = "CreateModel")]
    public async Task<ActionResult<Model>> Create(string modelId, CreateModelRequest request)
    {
        var result = await _modelService.Create(modelId, request);
        if (result.Succeeded()) return result.Value;
        return MapError(result.Error);
    }

    /// <summary>
    /// Deletes a model. Models which are currently being trained cannot be deleted.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <returns></returns>
    [HttpDelete("{modelId}", Name = "DeleteModel")]
    public async Task<ActionResult> Delete(string modelId)
    {
        var result = await _modelService.Delete(modelId);
        if (result.Succeeded()) return Ok();
        return MapError(result.Error);
    }

    /// <summary>
    /// Trains the specified model with the provided parameter.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("{modelId}/train", Name = "TrainModel")]
    public async Task<ActionResult> Train(string modelId, TrainModelRequest request)
    {
        var result = await _trainingService.Train(modelId, request);
        if (result.Succeeded()) return Ok();
        return MapError(result.Error);
    }

    /// <summary>
    /// Used to make an inference with the specified model.
    /// </summary>
    /// <param name="modelId">The id of the model.</param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("{modelId}/infer", Name = "Infer")]
    public async Task<ActionResult<Inference>> Infer(string modelId, [FromForm] InferRequest request)
    {
        var result = await _inferenceService.Infer(modelId, request);
        if (result.Succeeded()) return Ok();
        return MapError(result.Error);
    }
}
