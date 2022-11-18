using DucksAndDogs.Core.Models;
using DucksAndDogs.Core.Services;

namespace DucksAndDogs.Application.ML.Services;

public class ModelTrainingService : IModelTrainingService
{
    private readonly IModelService _modelService;

    public ModelTrainingService(IModelService modelService) {
        _modelService = modelService;
    }

    public async Task<Result> TrainModelsInQueueAsync(CancellationToken cancellationToken)
    {
        var result = await _modelService.ListAsync();
        if (!result.Succeeded()) return (Result)result;

        var models = result.Value.Models;
        var modelsForTraining = models.Where(x => x.Status == ModelStatus.Training);
        if (!modelsForTraining.Any()) return Result.Success();

        foreach (var model in modelsForTraining)  
        {
            var trainingResult = await TrainModelAsync(model.Id);
            if (!trainingResult.Succeeded()) return trainingResult;
        }

        return Result.Success();
    }

    private Task<Result> TrainModelAsync(string id)
    {
        throw new NotImplementedException();
    }
}