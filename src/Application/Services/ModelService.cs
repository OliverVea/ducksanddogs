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
    public async Task<Result<Model>> Create(string modelId, CreateModelRequest request)
    {
        var getExisting = await Get(modelId);

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

        var result = await _store.Create(model);
        if (!result.Succeeded()) return Result<Model>.Failed(result.Error);

        return Result<Model>.Success(model);
    }

    public async Task<Result> Delete(string modelId)
    {
        var getExisting = await Get(modelId);
        if (!getExisting.Succeeded()) return Result.Failed(getExisting.Error);

        var model = getExisting.Value;
        if (model.Status is ModelStatus.Training)
        {
            var error = new Error(400, "modelId", $"Model with modelId '{modelId}' is currently being trained.");
            return Result.Failed(error);
        }

        return await _store.Delete(modelId);
    }

    public Task<Result<Model>> Get(string modelId)
    {
        return _store.Get(modelId);
    }

    public Task<Result<ModelList>> List()
    {
        return _store.List();
    }
}