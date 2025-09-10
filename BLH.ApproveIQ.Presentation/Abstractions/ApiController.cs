using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLH.ApproveIQ.Application;
using BLH.ApproveIQ.Domain.Models;
using BLH.ApproveIQ.Domain.Shared;

namespace BLH.ApproveIQ.Presentation.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender) => Sender = sender;

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(
                    CreateProblemDetails(
                        "Validation Error", StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),
            _ =>
                BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }

    protected IActionResult HandlePagedResult<T>(Result<PagedResult<T>> result)
    {
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
}
