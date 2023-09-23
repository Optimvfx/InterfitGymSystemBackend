using GymCardSystemBackend.Middlewares.ErrorHandler;

namespace GymCardSystemBackend.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ErrorHandlerCollection _errorHandler;

    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly LogLevel _logLevel;

    public ErrorHandlerMiddleware(RequestDelegate next, ErrorHandlerCollection errorHandler, ILogger<ErrorHandlerMiddleware> logger, LogLevel logLevel)
    {
        _next = next;
        _errorHandler = errorHandler;
        
        _logger = logger;
        _logLevel = logLevel;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HandleExceptionAsync(context, ex);
        }
    }

    private void HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.Log(_logLevel, exception.ToString());
        
        _errorHandler.Handle(context, exception);
    }
}
