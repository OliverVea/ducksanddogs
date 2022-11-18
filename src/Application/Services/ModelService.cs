using DucksAndDogs.Core.Models;
using DucksAndDogs.Core.Services;
using DucksAndDogs.Core.Stores;

namespace DucksAndDogs.Application.Services;

public class ModelService : IModelService
{
    private IModelStore _store;
    public ModelService(IModelStore store)
    {
        _store = store;
    }
    public async Task<Result<Model>> CreateAsync(string modelId, CreateModelRequest request)
    {
        var getExisting = await GetAsync(modelId);

        if (getExisting.Succeeded())
        {
            var error = new Error(409, "modelId", $"Model with modelId '{modelId}' already exists.");
            return Result<Model>.Failed(error);
        }

        if (getExisting.Error.StatusCode != 404) return getExisting;

        var model = new Model
        {
            Id = modelId,
            Name = request.Name,
            Status = ModelStatus.Untrained
        };

        var result = await _store.CreateAsync(model);
        if (!result.Succeeded()) return Result<Model>.Failed(result.Error);

        return Result<Model>.Success(model);
    }

    public async Task<Result> DeleteAsync(string modelId)
    {
        var getExisting = await GetAsync(modelId);
        if (!getExisting.Succeeded()) return Result.Failed(getExisting.Error);

        var model = getExisting.Value;
        if (model.Status is ModelStatus.Training)
        {
            var error = new Error(400, "modelId", $"Model with modelId '{modelId}' is currently being trained.");
            return Result.Failed(error);
        }

        return await _store.DeleteAsync(modelId);
    }

    public Task<Result<Model>> GetAsync(string modelId)
    {
        return _store.GetAsync(modelId);
    }

    public Task<Result<ModelList>> ListAsync()
    {
        return _store.ListAsync();
    }

    public async Task<Result> SetStatusAsync(string modelId, ModelStatus newStatus)
    {
        var getExisting = await GetAsync(modelId);
        if (!getExisting.Succeeded()) return Result.Failed(getExisting.Error);

        var model = getExisting.Value;

        if (model.Status == ModelStatus.Untrained && newStatus == ModelStatus.Ready)
            return Result.Failed(new Error(400, "", $"Cannot move status of model with id {modelId} from Untrained directly to Ready."));
        
        return await _store.SetStatusAsync(modelId, newStatus);
    }
}