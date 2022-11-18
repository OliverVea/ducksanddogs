using DucksAndDogs.Core.Models;
using DucksAndDogs.Core.Services;

namespace DucksAndDogs.Application.ML.Services;

public class ModelInferenceService : IModelInferenceService
{
    public Task<Result<Inference>> Infer(string modelId, InferRequest request)
    {
        throw new NotImplementedException();
    }
}