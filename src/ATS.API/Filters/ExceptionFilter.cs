using ATS.Contracts.Responses;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ATS.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CustomException)
            HandleCustomException(context);
        else
            HandleUnknownException(context);
    }

    private void HandleCustomException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
        {
            var exception = context.Exception as ErrorOnValidationException;

            _logger.LogWarning(ErrorMessages.Errors.ValidationErrorLog,
                context.HttpContext.Request.Path,
                exception!.Messages);

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new JsonErrorResponse(exception!.Messages));
        }
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, ErrorMessages.Errors.UnknownErrorLog,
            context.HttpContext.Request.Path);

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new JsonErrorResponse(ErrorMessages.Errors.UnknownError));
    }
}
