using System.Diagnostics.CodeAnalysis;

namespace DucksAndDogs.Core.Models;

/// <summary>
/// Base class for results.
/// </summary>
/// <value></value>
public abstract record ResultBase 
{
    /// <summary>
    /// The error, if any arose while executing the operation.
    /// </summary>
    public Error? Error;

    /// <summary>
    /// Constructor for successful results.
    /// </summary>
    protected ResultBase() { }

    /// <summary>
    /// Constructor for erroneous results.
    /// </summary>
    /// <param name="error">The error which made the operation fail.</param>
    protected ResultBase(Error error) => Error = error;

    /// <summary>
    /// Checks if the operation was successful.
    /// </summary>
    /// <returns></returns>
    [MemberNotNullWhen(false, nameof(Error))]
    public bool Succeeded() => Error == null;
}

/// <summary>
/// A general result of an operation.
/// </summary>
/// <value></value>
public record Result : ResultBase
{
    /// <summary>
    /// Utility method for getting a successful result with an easy-to-read method.
    /// </summary>
    /// <returns></returns>
    public static Result Success() => new Result();

    /// <summary>
    /// Utility method for getting a failed result with an easy-to-read method.
    /// </summary>
    /// <returns></returns>
    public static Result Failed(Error error) => new Result { Error = error };
}

/// <summary>
/// A specialized result used when the operation has a return value.
/// </summary>
/// <value></value>
public record Result<TResult> : ResultBase
{
    /// <summary>
    /// The resulting value. Will be the default value (possibly null) if the operation was not successful.
    /// </summary>
    /// <returns></returns>
    public TResult Value = default(TResult)!;

    /// <summary>
    /// Utility method for getting a successful result with an easy-to-read method.
    /// </summary>
    /// <returns></returns>
    public static Result<TResult> Success(TResult value) => new Result<TResult> { Value = value };

    /// <summary>
    /// Utility method for getting a failed result with an easy-to-read method.
    /// </summary>
    /// <returns></returns>
    public static Result<TResult> Failed(Error error) => new Result<TResult> { Error = error };
}
