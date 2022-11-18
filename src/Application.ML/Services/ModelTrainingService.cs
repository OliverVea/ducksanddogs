using DucksAndDogs.Core.Models;
using DucksAndDogs.Core.Services;

namespace DucksAndDogs.Application.ML.Services;

public class ModelTrainingService : IModelTrainingService
{
    public Task<Result> Train(string modelId, TrainModelRequest request)
    {
        throw new NotImplementedException();
    }
}