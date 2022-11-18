namespace DucksAndDogs.Core.Models;

/// <summary>
/// Class used for describing errors which can be returned to the endpoint caller.
/// </summary>
/// <value></value>
public record Error
{
    /// <summary>
    /// Constructor used to create an Error instance.
    /// </summary>
    /// <param name="statusCode">The error status code.</param>
    /// <param name="key">A key usually specifying the problematic field.</param>
    /// <param name="errorMessage">The error message.</param>
    public Error(int statusCode, string key, string errorMessage)
    {
        StatusCode = statusCode;
        Key = key;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// The error status code.
    /// </summary>
    /// <value>404</value>
    public int StatusCode { get; set; }

    /// <summary>
    /// A key usually specifying the problematic field.
    /// </summary>
    /// <value>modelId</value>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// The error message.
    /// </summary>
    /// <value>Could not find model with id 'my-id'.</value>
    public string ErrorMessage { get; set; } = string.Empty;
}