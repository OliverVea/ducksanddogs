namespace DucksAndDogs.Core.Models;

/// <summary>
/// The status of a model.
/// </summary>
public enum ModelStatus
{
    /// <summary>
    /// The model has been registered but not yet trained.
    /// </summary>
    Untrained,
    
    /// <summary>
    /// The model is ready to be used for inference.
    /// </summary>
    Ready,
    
    /// <summary>
    /// The model is queued for training.
    /// </summary>
    Queued,
    
    /// <summary>
    /// The model is currently being trained.
    /// </summary>
    Training
}