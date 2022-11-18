using DucksAndDogs.Core.Services;
using Microsoft.Extensions.Hosting;

namespace DucksAndDogs.Jobs.Jobs;

public class TrainingJob : BackgroundService
{
    private const int SleepMilliseconds = 5000;
    private readonly IModelTrainingService _modelTrainingService;

    public TrainingJob(IModelTrainingService modelTrainingService) {
        _modelTrainingService = modelTrainingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested) 
        {
            await _modelTrainingService.TrainModelsInQueueAsync(stoppingToken);
            await Task.Delay(SleepMilliseconds);
        }
    }
}