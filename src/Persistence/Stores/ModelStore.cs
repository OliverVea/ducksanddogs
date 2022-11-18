using DucksAndDogs.Core.Models;
using DucksAndDogs.Core.Stores;
using DucksAndDogs.Persistence.Models;
using System.Text.Json;

namespace DucksAndDogs.Persistence.Stores;

public class ModelStore : IModelStore
{
    private const int SemaphoreTimeoutMilliseconds = 5000;

    private static readonly SemaphoreSlim _jsonFileSemaphore = new SemaphoreSlim(1, 1);

    public async Task<Result> Create(Core.Models.Model model)
    {
        var error = await GetSemaphoreAsync();
        if (error != null) return Result.Failed(error);

        try
        {
            var result = await ReadJsonFile();
            if (!result.Succeeded())
            {
                error = new Error(500, "", "Failed reading models file.");
                return Result.Failed(error);
            }

            var newData = result.Value with
            {
                Models = result.Value.Models.Append(Map(model)).ToArray()
            };

            var writeResult = await WriteJsonFile(newData);
            if (!writeResult.Succeeded())
            {
                error = new Error(500, "", "Failed writing models file.");
                return Result.Failed(error);
            }

            return Result.Success();
        }
        catch (Exception e) {
            error = new Error(500, "", $"Failed creating model with exception:\n{e}");
            return Result.Failed(error);
        }
        finally
        {
            _jsonFileSemaphore.Release();
        }
    }

    public async Task<Result> Delete(string modelId)
    {
        var error = await GetSemaphoreAsync();
        if (error != null) return Result.Failed(error);
        
        try {
            var readResult = await ReadJsonFile();
            if (!readResult.Succeeded())
            {
                error = new Error(500, "", "Failed reading models file.");
                return Result.Failed(error);
            }

            var newData = readResult.Value with
            {
                Models = readResult.Value.Models.Where(x => x.Id != modelId).ToArray()
            };

            var writeResult = await WriteJsonFile(newData);
            if (!writeResult.Succeeded())
            {
                error = new Error(500, "", "Failed writing models file.");
                return Result.Failed(error);
            }

            return Result.Success();
        }
        catch (Exception e) {
            error = new Error(500, "", $"Failed deleting model with exception:\n{e}");
            return Result.Failed(error);
        }
        finally {
            _jsonFileSemaphore.Release();
        }
    }

    public async Task<Result<Core.Models.Model>> Get(string modelId)
    {
        var error = await GetSemaphoreAsync();
        if (error != null) return Result<Core.Models.Model>.Failed(error);
        
        try {
            var result = await ReadJsonFile();
            if (!result.Succeeded())
            {
                error = new Error(500, "", "Failed reading models file.");
                return Result<Core.Models.Model>.Failed(error);
            }

            var model = result.Value.Models
                .Where(x => x.Id == modelId)
                .Select(Map)
                .SingleOrDefault();

            if (model == null) {
                error = new Error(404, "modelId", $"Could not find model with id {modelId}.");
                return Result<Core.Models.Model>.Failed(error);
            }

            return Result<Core.Models.Model>.Success(model);
        }
        catch (Exception e) {
            error = new Error(500, "", $"Failed getting model with exception:\n{e}");
            return Result<Core.Models.Model>.Failed(error);
        }
        finally {
            _jsonFileSemaphore.Release();
        }
    }

    public async Task<Result<ModelList>> List()
    {
        var error = await GetSemaphoreAsync();
        if (error != null) return Result<ModelList>.Failed(error);
        
        try {
            var result = await ReadJsonFile();
            if (!result.Succeeded())
            {
                error = new Error(500, "", "Failed reading models file.");
                return Result<ModelList>.Failed(error);
            }

            var models = result.Value.Models.Select(Map).ToList();
            var modelList = new ModelList 
            {
                Models = models
            };

            return Result<ModelList>.Success(modelList);
        }
        catch (Exception e) {
            error = new Error(500, "", $"Failed getting model with exception:\n{e}");
            return Result<ModelList>.Failed(error);
        }
        finally {
            _jsonFileSemaphore.Release();
        }
    }

    private async Task<Result<ModelData>> ReadJsonFile()
    {
        var fileStream = File.OpenRead(Constants.ModelDataPath);
        try {
            var data = await JsonSerializer.DeserializeAsync<ModelData>(fileStream);
            if (data == null) {
                var error = new Error(500, "", "Could not deserialize ModelData file.");
                return Result<ModelData>.Failed(error);
            }

            return Result<ModelData>.Success(data);
        }
        finally 
        {
            fileStream.Close();
        }
    }

    private async Task<Result> WriteJsonFile(ModelData data)
    {
        File.Delete(Constants.ModelDataPath);
        var fileStream = File.OpenWrite(Constants.ModelDataPath);
        try {
            await JsonSerializer.SerializeAsync<ModelData>(fileStream, data);
            
            return Result.Success();
        }
        finally 
        {
            fileStream.Close();
        }
    }

    private async Task<Error?> GetSemaphoreAsync() 
    { 
        var gotSemaphore = await _jsonFileSemaphore.WaitAsync(SemaphoreTimeoutMilliseconds);
        if (!gotSemaphore) return new Error(500, "", "Could not get access to json file.");
        return null;
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