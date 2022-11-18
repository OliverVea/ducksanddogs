using System.Text.Json;
using DucksAndDogs.Core.Models;

namespace DucksAndDogs.Persistence.Stores;

public abstract class StoreBase<T>
{
    protected abstract string DataPath { get; }

    protected static async Task<R> DoWithSemaphoreAsync<R>(SemaphoreSlim semaphore, Func<Task<R>> func, int semaphoreTimeoutmillisecodns = 5000) where R : ResultBase, new()
    {
        var gotSemaphore = await semaphore.WaitAsync(semaphoreTimeoutmillisecodns);
        if (!gotSemaphore) return new R() { Error = new Error(500, "", "Could not get access to json file.") };

        try
        {
            return await func();
        }
        catch (Exception e)
        {
            return new R() { Error = new Error(500, "", $"Failed deleting model with exception:\n{e}") };
        }
        finally
        {
            semaphore.Release();
        }
    }

    protected async Task<Result<T>> ReadJsonFileAsync()
    {
        var fileStream = File.OpenRead(DataPath);
        Error error;
        try
        {
            var data = await JsonSerializer.DeserializeAsync<T>(fileStream);
            if (data != null) return Result<T>.Success(data);

            error = new Error(500, "", "Could not deserialize file.");
        }
        catch (Exception e) { error = new Error(500, "", $"Could not deserialize file. Got exception:\n{e}"); }
        finally { fileStream.Close(); }

        return Result<T>.Failed(error);
    }

    protected async Task<Result> WriteJsonFileAsync(T data)
    {
        File.Delete(DataPath);
        var fileStream = File.OpenWrite(DataPath);
        try
        {
            await JsonSerializer.SerializeAsync(fileStream, data);
            return Result.Success();
        }
        finally
        {
            fileStream.Close();
        }
    }
}
