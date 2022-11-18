using DucksAndDogs.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DucksAndDogs.Api.Controllers;

/// <summary>
/// Base for all DucksAndDogs Api controllers.
/// </summary>
public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    /// <summary>
    /// Maps an error to the appropriate ActionResult.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    protected ActionResult MapError(Error error)
    {
        ModelState.AddModelError(error.Key, error.ErrorMessage);

        return error.StatusCode switch
        {
            400 => BadRequest(ModelState),
            404 => NotFound(ModelState),
            409 => Conflict(ModelState),
            500 => ServiceUnavailable(ModelState),
            503 => InternalServerError(ModelState),
            _ => InternalServerError(ModelState)
        };
    }

    /// <summary>
    /// Returns InternalServerError ObjectResult.
    /// </summary>
    /// <param name="modelState"></param>
    /// <returns></returns>
    protected ObjectResult InternalServerError(ModelStateDictionary modelState)
        => GetObjectResult(modelState, 500);

    /// <summary>
    /// Returns ServiceUnavailable ObjectResult.
    /// </summary>
    /// <param name="modelState"></param>
    /// <returns></returns>
    protected ObjectResult ServiceUnavailable(ModelStateDictionary modelState)
        => GetObjectResult(modelState, 503);

    private ObjectResult GetObjectResult(ModelStateDictionary modelState, int statusCode)
    {
        var problemDetails = CreateProblemDetails(modelState, statusCode);
        return new ObjectResult(problemDetails) { StatusCode = statusCode };
    }

    private ValidationProblemDetails CreateProblemDetails(ModelStateDictionary modelState, int statusCode)
    {
        var problemDetails = ProblemDetailsFactory.CreateProblemDetails(HttpContext, statusCode);

        return new ValidationProblemDetails(modelState)
        {
            Title = problemDetails.Title,
            Type = problemDetails.Type
        };

    }
}