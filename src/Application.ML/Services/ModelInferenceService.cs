using DucksAndDogs.Core.Models;
using DucksAndDogs.Core.Services;

namespace DucksAndDogs.Application.ML.Services;

public class ModelInferenceService : IModelInferenceService
{
    public Task<Result<Inference>> MakeInferenceAsync(string modelId, InferRequest request)
    {
        throw new NotImplementedException();
    }
}