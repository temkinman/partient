using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using Hospital.Application.Exceptions;

namespace Hospital.Application.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = StatusCodes.Status500InternalServerError;
        var result = exception.InnerException?.Message ?? exception.Message;
        
        switch (exception)
        {
            case ValidationException validationException:
                code = StatusCodes.Status400BadRequest;
                
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                result =  JsonSerializer.Serialize(errors);
                _logger.LogError(exception, "ValidationException was occured");
                break;
            case NotFoundException:
                code = StatusCodes.Status404NotFound;
                _logger.LogError(exception, "NotFoundException was occured");
                break;
            case ConflictException conflictException:
                code = StatusCodes.Status409Conflict;
                result = JsonSerializer.Serialize(conflictException.Message);
                _logger.LogError(exception, "ConflictException was occured");
                break;
            case TaskCanceledException:
            case OperationCanceledException:
                code = 499;
                result = JsonSerializer.Serialize(exception.Message);
                _logger.LogInformation(exception, "Operation was canceled");
                break;
            default:
                _logger.LogError(exception, "An unexpected error occurred");
                break;
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}
