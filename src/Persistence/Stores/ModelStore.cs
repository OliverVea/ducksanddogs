using DucksAndDogs.Core.Models;
using DucksAndDogs.Core.Stores;
using DucksAndDogs.Persistence.Models;

namespace DucksAndDogs.Persistence.Stores;

public class ModelStore : StoreBase, IModelStore
{
    private static readonly SemaphoreSlim _jsonFileSemaphore = new SemaphoreSlim(1, 1);

    public Task<Result> Create(Core.Models.Model model)
    {
        return DoWithSemaphore(_jsonFileSemaphore, async () => {
            var result = await ReadJsonFile<ModelData>(Constants.ModelDataPath);
            if (!result.Succeeded()) return (Result)result;

            var data = result.Value;

            var newData = data with
            {
                Models = data.Models.Append(Map(model)).ToArray()
            };

            var writeResult = await WriteJsonFile(newData, Constants.ModelDataPath);
            if (!writeResult.Succeeded()) return writeResult;

            return Result.Success();
        });
    }

    public Task<Result> Delete(string modelId)
    {
        return DoWithSemaphore(_jsonFileSemaphore, async () => {
            var readResult = await ReadJsonFile<ModelData>(Constants.ModelDataPath);
            if (!readResult.Succeeded()) return (Result)readResult;

            var newData = readResult.Value with
            {
                Models = readResult.Value.Models.Where(x => x.Id != modelId).ToArray()
            };

            var writeResult = await WriteJsonFile(newData, Constants.ModelDataPath);
            if (!writeResult.Succeeded()) return writeResult;

            return Result.Success();
        });
    }

    public Task<Result<Core.Models.Model>> Get(string modelId)
    {
        return DoWithSemaphore(_jsonFileSemaphore, async () => {
            var result = await ReadJsonFile<ModelData>(Constants.ModelDataPath);
            if (!result.Succeeded()) 
                return Result<Core.Models.Model>.Failed(new Error(500, "", "Failed reading models file."));

            var model = result.Value.Models
                .Where(x => x.Id == modelId)
                .Select(Map)
                .FirstOrDefault();

            if (model == null) 
                return Result<Core.Models.Model>.Failed(new Error(404, "modelId", $"Could not find model with id {modelId}."));

            return Result<Core.Models.Model>.Success(model);
        });
    }

    public Task<Result<ModelList>> List()
    {
        return DoWithSemaphore(_jsonFileSemaphore, async () => {
             var result = await ReadJsonFile<ModelData>(Constants.ModelDataPath);
            if (!result.Succeeded())
                return Result<ModelList>.Failed(new Error(500, "", "Failed reading models file."));

            var models = result.Value.Models.Select(Map).ToList();
            var modelList = new ModelList
            {
                Models = models
            };

            return Result<ModelList>.Success(modelList);
        });
    }
    


    private Persistence.Models.Model MapCreateRequest(string modelId, CreateModelRequest request)
        => new Models.Model
        {
            Id = modelId,
            Name = request.Name,
            Status = ModelStatus.Untrained
        };

    private Core.Models.Model Map(Persistence.Models.Model model)
        => new Core.Models.Model
        {
            Id = model.Id,
            Name = model.Name,
            Status = model.Status
        };

    private Persistence.Models.Model Map(Core.Models.Model model)
        => new Persistence.Models.Model
        {
            Id = model.Id,
            Name = model.Name,
            Status = model.Status
        };
}