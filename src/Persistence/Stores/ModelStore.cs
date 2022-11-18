using DucksAndDogs.Core.Models;
using DucksAndDogs.Core.Stores;
using DucksAndDogs.Persistence.Models;

namespace DucksAndDogs.Persistence.Stores;

// Yes, this should probably interact with some sort of actual database and not a .json file, but I thought it was fun to make a .json file sort of thread safe.
sealed class ModelStore : StoreBase<ModelData>, IModelStore, IInitializableStore
{
    private static readonly SemaphoreSlim _jsonFileSemaphore = new SemaphoreSlim(1, 1);

    protected override string DataPath => "./data/ModelData.json";

    public async Task InitializeStoreAsync()
    {
        if (File.Exists(DataPath)) return;
    
        var fileStream = File.OpenWrite(DataPath);
        await WriteJsonFileAsync(new ModelData());
    }

    public Task<Result> CreateAsync(Core.Models.Model model)
    {
        return DoWithSemaphoreAsync(_jsonFileSemaphore, async () => {
            var result = await ReadJsonFileAsync();
            if (!result.Succeeded()) return (Result)result;

            var data = result.Value;
            data.Models = data.Models.Append(Map(model)).ToArray();

            return await WriteJsonFileAsync(data);
        });
    }

    public Task<Result> DeleteAsync(string modelId)
    {
        return DoWithSemaphoreAsync(_jsonFileSemaphore, async () => {
            var readResult = await ReadJsonFileAsync();
            if (!readResult.Succeeded()) return (Result)readResult;

            var data = readResult.Value;
            data.Models = data.Models.Where(x => x.Id != modelId).ToArray();

            return await WriteJsonFileAsync(data);
        });
    }

    public Task<Result<Core.Models.Model>> GetAsync(string modelId)
    {
        return DoWithSemaphoreAsync(_jsonFileSemaphore, async () => {
            var result = await ReadJsonFileAsync();
            if (!result.Succeeded()) return Result<Core.Models.Model>.Failed(result.Error);

            var model = result.Value.Models
                .Where(x => x.Id == modelId)
                .Select(Map)
                .FirstOrDefault();

            if (model == null) return Result<Core.Models.Model>.Failed(new Error(404, "modelId", $"Could not find model with id {modelId}."));
            return Result<Core.Models.Model>.Success(model);
        });
    }

    public Task<Result<ModelList>> ListAsync()
    {
        return DoWithSemaphoreAsync(_jsonFileSemaphore, async () => {
            var result = await ReadJsonFileAsync();
            if (!result.Succeeded()) return Result<ModelList>.Failed(result.Error);

            var modelList = Map(result.Value.Models);

            return Result<ModelList>.Success(modelList);
        });
    }

    public Task<Result> SetStatusAsync(string modelId, ModelStatus newStatus)
    {
        return DoWithSemaphoreAsync(_jsonFileSemaphore, async () => {
            var result = await ReadJsonFileAsync();
            if (!result.Succeeded()) return (Result)result;

            var data = result.Value;
            var model = data.Models.Where(x => x.Id == modelId).FirstOrDefault();
            if (model == null) return Result.Failed(new Error(404, "modelId", $"Could not find model with id {modelId}."));

            model.Status = newStatus;

            return await WriteJsonFileAsync(data);
        });
    }
    


    private Persistence.Models.Model Map(string modelId, CreateModelRequest request)
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

    private ModelList Map(Persistence.Models.Model[] models) 
        => new ModelList
        {
            Models =  models.Select(Map).ToList()
        };
}